using Steamworks;

namespace PSRMEssentials.Models
{
    public class TPARequest
    {
        public CSteamID Requester { get; set; }
        public CSteamID Target { get; set; }
    }
}