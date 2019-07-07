namespace Dnd.WebHost.Core.Models
{
    using System;

    public class PackageVersion
    {
        public string Version { get; set; }

        public string Sha { get; set; }

        public DateTimeOffset PublishDate { get; set; }
    }
}
