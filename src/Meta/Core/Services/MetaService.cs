// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetaService.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the MetaService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.Meta.Core.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Dnd.Meta.Core.Abstractions;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Default implementation of the Meta service.
    /// </summary>
    internal class MetaService : IMetaService
    {
        private readonly ILogger<MetaService> logger;

        public MetaService(ILogger<MetaService> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task<CliSettings> GetSettingsAsync()
        {
            var fileInfo = new FileInfo(this.GetPath());
            if (!fileInfo.Exists)
            {
                this.logger.LogError("Settings file not found at {path}", fileInfo.FullName);
                throw new AzureDevOpsMissingSettingsException($"Settings file not found");
            }

            using (var streamReader = File.OpenText(fileInfo.FullName))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var settings = (await JToken.ReadFromAsync(jsonReader)).ToObject<CliSettings>();
                    if (string.IsNullOrEmpty(settings?.AzureDevOps.Organization))
                    {
                        throw new AzureDevOpsMissingSettingsException("Organization name missing in the Azure DevOps settings ");
                    }

                    if (string.IsNullOrEmpty(settings?.AzureDevOps?.Credentials?.Username)
                        || string.IsNullOrEmpty(settings.AzureDevOps?.Credentials?.Token))
                    {
                        throw new AzureDevOpsMissingSettingsException("Credentials missing in the Azure DevOps settings ");
                    }

                    return settings;
                }
            }
        }

        /// <inheritdoc />
        public async Task SaveAzureCredentialsAsync(string organization, string username, string token)
        {
            var settings = new CliSettings();
            settings.AzureDevOps.Organization = organization;
            settings.AzureDevOps.Credentials.Token = token;
            settings.AzureDevOps.Credentials.Username = username;
            var jsonToken = JToken.FromObject(settings);
            var fileInfo = new FileInfo(this.GetPath());
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            using (var stream = File.Open(fileInfo.FullName, FileMode.OpenOrCreate))
            {
                stream.SetLength(0);
                using (var streamWriter = new StreamWriter(stream))
                {
                    using (var writer = new JsonTextWriter(streamWriter))
                    {
                        await jsonToken.WriteToAsync(writer);
                        await writer.FlushAsync();
                    }
                }
            }
        }

        private string GetPath() =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "dnd",
                "settings.json");
    }
}