namespace Dnd.WebHost.Core.Models
{
    using System.Collections.Generic;

    public class Package
    {
        public string Id { get; set; }

        public IDictionary<string, PackageVersion[]> Versions { get; set; } =
            new Dictionary<string, PackageVersion[]>();
    }
}