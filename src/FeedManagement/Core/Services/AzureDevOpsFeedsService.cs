// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureDevOpsFeedsService.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AzureDevOpsFeedsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Dnd.FeedManagement.Core.Abstractions;
    using Dnd.Meta.Core;
    using Dnd.Meta.Core.Abstractions;

    using Newtonsoft.Json;

    /// <summary>
    /// Default implementation for the <see cref="IAzureDevOpsFeedsService"/>
    /// </summary>
    internal class AzureDevOpsFeedsService : IAzureDevOpsFeedsService
    {
        private readonly HttpClient httpClient;

        private readonly IMetaService metaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsFeedsService"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="metaService">
        /// The meta Service.
        /// </param>
        public AzureDevOpsFeedsService(HttpClient httpClient, IMetaService metaService)
        {
            this.httpClient = httpClient;
            this.metaService = metaService;
        }

        /// <inheritdoc />
        public async Task<GetFeedsResult> GetFeedsAsync(ApiUser user)
        {
            var settings = await this.metaService.GetSettingsAsync();
            return await this.ExecuteGetAsync<GetFeedsResult>(
                $"https://feeds.dev.azure.com/{settings.AzureDevOps.Organization}/_apis/packaging/feeds?api-version=5.0-preview.1");
        }

        /// <inheritdoc />
        public async Task<GetFeedViewsResult> GetFeedViewsAsync(ApiUser user, Guid feedId)
        {
            var settings = await this.metaService.GetSettingsAsync();
            return await this.ExecuteGetAsync<GetFeedViewsResult>(
                $"https://feeds.dev.azure.com/{settings.AzureDevOps.Organization}/_apis/packaging/Feeds/{feedId}/views?api-version=5.0-preview.1");
        }

        /// <inheritdoc />
        public async Task<GetFeedViewResult> GetFeedViewAsync(ApiUser user, Guid feedId, Guid viewId)
        {
            var settings = await this.metaService.GetSettingsAsync();
            return await this.ExecuteGetAsync<GetFeedViewResult>($"https://feeds.dev.azure.com/{settings.AzureDevOps.Organization}/_apis/packaging/Feeds/{feedId}/views/{viewId}?api-version=5.0-preview.1");
        }

        /// <inheritdoc />
        public Task<GetFeedViewPackagesResult> GetFeedViewPackagesAsync(ApiUser user, string link) =>
            this.ExecuteGetAsync<GetFeedViewPackagesResult>(link);

        /// <inheritdoc />
        public Task<GetFeedViewPackageVersionsResult> GetFeedViewPackageVersionsAsync(ApiUser user, string link) =>
            this.ExecuteGetAsync<GetFeedViewPackageVersionsResult>(link);

        private async Task<T> ExecuteGetAsync<T>(string url)
        {
            var settings = await this.metaService.GetSettingsAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var byteArray = Encoding.ASCII.GetBytes(
                $"{settings.AzureDevOps.Credentials.Username}:{settings.AzureDevOps.Credentials.Token}");
            request.Headers.Authorization =
                new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), Convert.ToBase64String(byteArray));
            var response = await this.httpClient.SendAsync(request);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}