// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAnalyzerWebHost.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAnalyzerWebHost type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Abstractions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the component to provide a web host to analyze the repository.
    /// </summary>
    public interface IAnalyzerWebHost
    {
        /// <summary>
        /// Runs the host.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> that can be awaited.
        /// </returns>
        Task RunAsync(string[] args);
    }
}