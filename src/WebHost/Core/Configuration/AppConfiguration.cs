namespace Dnd.WebHost.Core.Configuration
{
    public class AppConfiguration
    {
        public string ServerName { get; set; }
        
        public string[] Feeds { get; set; } = new string[0];
    }
}