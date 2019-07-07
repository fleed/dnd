// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnalyzerWebViewService.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AnalyzerWebViewService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Services
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Dnd.FeedManagement.Core.Abstractions;
    using Dnd.Meta.Core;
    using Dnd.Meta.Core.Abstractions;
    using Dnd.WebHost.Core.Abstractions;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    /// Default implementation of the <see cref="IAnalyzerWebViewService"/>.
    /// </summary>
    internal class AnalyzerWebViewService : IAnalyzerWebViewService
    {
        private static readonly Regex FeedRegex = new Regex(@"feeds:reset:\<(?<Name>([\w]*))\>");

        private static readonly TimeSpan DataCacheTimeout = TimeSpan.FromMinutes(3);

        private static readonly TimeSpan ResetCacheTimeout = TimeSpan.FromMinutes(1);

        private readonly ILogger<AnalyzerWebViewService> logger;

        private readonly IAzureDevOpsFeedsRestApiClient client;

        private readonly IMapper mapper;

        private readonly IMemoryCache cache;

        private readonly IMetaService metaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzerWebViewService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        /// <param name="cache">The cache.</param>
        /// <param name="metaService">The Meta service.</param>
        public AnalyzerWebViewService(
            ILogger<AnalyzerWebViewService> logger,
            IAzureDevOpsFeedsRestApiClient client,
            IMapper mapper,
            IMemoryCache cache,
            IMetaService metaService)
        {
            this.logger = logger;
            this.client = client;
            this.mapper = mapper;
            this.cache = cache;
            this.metaService = metaService;
        }

        /// <inheritdoc />
        public async Task<Models.Feed> GetFeedAsync(string name) =>
            await this.cache.GetOrCreateAsync(
                $"feeds:<{name}>",
                async entry =>
                    {
                        var credentials = (await this.metaService.GetSettingsAsync())
                            .GetAzureDevOpsCredentialsOrThrowException();
                        entry.AbsoluteExpirationRelativeToNow = DataCacheTimeout;
                        this.UpdateReset(name);
                        return this.mapper.Map<Models.Feed>(
                            await this.client.GetFeedAsync(
                                new ApiUser(credentials.Username, credentials.Token),
                                name,
                                "Prerelease"));
                    });

        private void UpdateReset(string name)
        {
            var mo = new MemoryCacheEntryOptions();
            mo.RegisterPostEvictionCallback(this.PostEvictionCallback);
            mo.AddExpirationToken(
                new CancellationChangeToken(new CancellationTokenSource(ResetCacheTimeout).Token));
            this.cache.Set($"feeds:reset:<{name}>", DateTime.Now, mo);
        }
        
        private async void PostEvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            try
            {
                var credentials =
                    (await this.metaService.GetSettingsAsync()).GetAzureDevOpsCredentialsOrThrowException();
                var match = FeedRegex.Match(key.ToString());

                // Regenerate a set of updated data
                var feed = this.mapper.Map<Models.Feed>(
                    await this.client.GetFeedAsync(
                        new ApiUser(credentials.Username, credentials.Token),
                        match.Groups["Name"].Value,
                        "Prerelease"));

                this.cache.Set($"feeds:<{match.Groups["Name"].Value}>", feed, DataCacheTimeout);

                // Re-set the cache to be reloaded in 35min
                this.UpdateReset(match.Groups["Name"].Value);
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, "Error while fetching the packages feed.");
            }
        }
    }
}