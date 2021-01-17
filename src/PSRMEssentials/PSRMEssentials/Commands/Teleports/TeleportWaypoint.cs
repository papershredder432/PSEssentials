using System.Collections.Generic;
using Rocket.API;

namespace PSRMEssentials.Commands.Teleports
{
    public class TeleportWaypoint : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {

        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "TeleportWaypoint";
        public string Help => "Teleport to your waypoint.";
        public string Syntax => "/teleportwaypoint";
        public List<string> Aliases => new List<string> { "tpwp", "teleportwp" };
        public List<string> Permissions => new List<string> { "ps.essentials.teleports.teleportwaypoint" };
    }
}