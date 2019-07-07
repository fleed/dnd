// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureDevOpsFeedsRestApiClient.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AzureDevOpsFeedsRestApiClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Dnd.FeedManagement.Core.Abstractions;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Default implementation for the <see cref="IAzureDevOpsFeedsRestApiClient"/> interface.
    /// </summary>
    internal class AzureDevOpsFeedsRestApiClient : IAzureDevOpsFeedsRestApiClient
    {
        private static readonly Regex BranchRegex =
            new Regex(@"(?<Version>[\d]\.[\d]\.[\d])-(?<Branch>[\w-]+)\.[\d]-(?<BuildId>[\d]+)");

        private readonly IAzureDevOpsFeedsService azureDevOpsFeedsService;

        private readonly IMemoryCache cache;

        private readonly AzureDevOpsFeedsRestApiOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsFeedsRestApiClient"/> class.
        /// </summary>
        /// <param name="azureDevOpsFeedsService">
        /// The azure dev ops feeds service.
        /// </param>
        /// <param name="options">The options.</param>
        /// <param name="cache">The optional cache.</param>
        public AzureDevOpsFeedsRestApiClient(
            IAzureDevOpsFeedsService azureDevOpsFeedsService,
            IOptions<AzureDevOpsFeedsRestApiOptions> options,
            IMemoryCache cache = null)
        {
            this.azureDevOpsFeedsService = azureDevOpsFeedsService;
            this.cache = cache;
            this.options = options.Value;
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionForBranchAsync(ApiUser user, string feed, string view, string packageId, string branchName)
        {
            var result = this.cache != null && this.options.CacheFeedsInternally
                                                     ? await this.cache.GetOrCreateAsync(
                                                           $"<{feed}>.<{view}>",
                                                           async entry =>
                                                               {
                                                                   var p = await this.GetPackages(user, feed, view);
                                                                   entry.SetValue(p);
                                                                   var timeout = this.options.InternalCacheTimeout > TimeSpan.Zero
                                                                                     ? this.options.InternalCacheTimeout
                                                                                     : TimeSpan.FromMinutes(1);
                                                                   entry.SetAbsoluteExpiration(timeout);
                                                                   return p;
                                                               })
                                                     : await this.GetPackages(user, feed, view);
            var packages = new Package[result.Packages.Length];
            result.Packages.CopyTo(packages, 0);
            if (!string.IsNullOrEmpty(packageId))
            {
                packages = packages.Where(p => string.Equals(packageId, p.Name)).ToArray();
            }

            var newestPackages = new Dictionary<string, PackageVersion>();
            foreach (var package in packages)
            {
                var versions = await this.azureDevOpsFeedsService.GetFeedViewPackageVersionsAsync(user, package.Links.Versions.Href);
                var packagesOfBranch = versions.Versions.Where(v => string.IsNullOrEmpty(branchName) || v.Version.Contains(branchName));

                StoreNewestVersion(packagesOfBranch, newestPackages, package.Name);
            }

            if (!newestPackages.Any())
            {
                throw new PackageNotFoundException();
            }

            return CreateResultString(newestPackages);
        }

        /// <inheritdoc />
        public async Task<NugetFeed> GetFeedAsync(ApiUser user, string feed, string view)
        {
            var result = this.cache != null && this.options.CacheFeedsInternally
                             ? await this.cache.GetOrCreateAsync(
                                   $"<{feed}>.<{view}>",
                                   async entry =>
                                       {
                                           var p = await this.GetPackages(user, feed, view);
                                           entry.SetValue(p);
                                           var timeout = this.options.InternalCacheTimeout > TimeSpan.Zero
                                                             ? this.options.InternalCacheTimeout
                                                             : TimeSpan.FromMinutes(1);
                                           entry.SetAbsoluteExpiration(timeout);
                                           return p;
                                       })
                             : await this.GetPackages(user, feed, view);
            var packages = new List<NugetPackage>();

            using (var semaphore = new SemaphoreSlim(5))
            {
                    var tasks = result.Packages.Where(p => p.Name.StartsWith(feed)).Select(
                        async package =>
                            {
                                // ReSharper disable once AccessToDisposedClosure
                                await semaphore.WaitAsync();
                                try
                                {
                                    packages.Add(
                                        new NugetPackage
                                            {
                                                Id = package.Name,
                                                Versions =
                                                    (await this.azureDevOpsFeedsService.GetFeedViewPackageVersionsAsync(
                                                         user,
                                                         package.Links.Versions.Href)).Versions
                                                    .Select(
                                                        v => new Tuple<string, NugetPackageVersion>(
                                                            GetBranch(v.Version),
                                                            new NugetPackageVersion { Version = v.Version, PublishDate = v.PublishDate }))
                                                    .GroupBy(p => p.Item1).ToDictionary(
                                                        g => g.Key,
                                                        g => g.Select(i => i.Item2).ToArray())
                                            });
                                }
                                finally
                                {
                                    // ReSharper disable once AccessToDisposedClosure
                                    semaphore.Release();
                                }
                            });

                    await Task.WhenAll(tasks);
            }

            return new NugetFeed
                       {
                           Name = feed,
                           Packages = packages.ToDictionary(p => p.Id)
                       };
        }

        private static string GetBranch(string value)
        {
            var match = BranchRegex.Match(value);
            return match.Success ? match.Groups["Branch"].Value : "-";
        }

        private static string CreateResultString(Dictionary<string, PackageVersion> newestPackages)
        {
            // only provide version if exact search, better for scripting
            if (newestPackages.Count == 1)
            {
                return newestPackages.First().Value.Version;
            }

            // provide result list with package names if multiple results
            var resultList = new List<string>();
            foreach (var newestPackageVersion in newestPackages)
            {
                resultList.Add($"{newestPackageVersion.Key}: {newestPackageVersion.Value.Version}");
            }

            var result = string.Join($"{Environment.NewLine}", resultList);
            return result;
        }

        private static void StoreNewestVersion(IEnumerable<PackageVersion> packagesOfBranch, Dictionary<string, PackageVersion> newestPackages, string packageName)
        {
            foreach (var packageVersion in packagesOfBranch)
            {
                AddPackageIfNewOrNewer(newestPackages, packageName, packageVersion);
            }
        }

        private static void AddPackageIfNewOrNewer(IDictionary<string, PackageVersion> newestPackages, string packagename, PackageVersion packageVersion)
        {
            if (!newestPackages.TryGetValue(packagename, out var currentNewestPackage))
            {
                newestPackages[packagename] = packageVersion;
                return;
            }

            if (currentNewestPackage.PublishDate < packageVersion.PublishDate)
            {
                newestPackages[packagename] = packageVersion;
            }
        }

        private async Task<GetFeedViewPackagesResult> GetPackages(ApiUser user, string feed, string view)
        {
            var feeds = await this.azureDevOpsFeedsService.GetFeedsAsync(user);
            var feedId = feeds.Feeds.Single(f => string.Equals(feed, f.Name)).Id;
            var views = await this.azureDevOpsFeedsService.GetFeedViewsAsync(user, feedId);
            var viewId = views.FeedViews.Single(v => string.Equals(view, v.Name)).Id;
            var feedView = await this.azureDevOpsFeedsService.GetFeedViewAsync(user, feedId, viewId);
            return await this.azureDevOpsFeedsService.GetFeedViewPackagesAsync(user, feedView.Links.Packages.Href);
        }
    }
}