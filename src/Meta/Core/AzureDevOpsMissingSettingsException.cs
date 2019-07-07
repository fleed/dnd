// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureDevOpsMissingSettingsException.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AzureDevOpsMissingSettingsException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.Meta.Core
{
    using System;

    /// <summary>
    /// Exception thrown when the Azure DevOps required settings were not found.
    /// </summary>
    public class AzureDevOpsMissingSettingsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsMissingSettingsException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public AzureDevOpsMissingSettingsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsMissingSettingsException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AzureDevOpsMissingSettingsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsMissingSettingsException"/> class.
        /// </summary>
        public AzureDevOpsMissingSettingsException()
        {
        }
    }
}