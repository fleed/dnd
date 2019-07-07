// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiUser.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the ApiUser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    /// <summary>
    /// Defines the authentication properties for the user of the APIs.
    /// </summary>
    public class ApiUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiUser"/> class.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public ApiUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiUser"/> class.
        /// </summary>
        public ApiUser()
        {
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}