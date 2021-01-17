using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.Warps
{
    public class Warps : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;

            if (PSRMEssentials.Instance.WarpsDatabase.Warps.Count == 0)
            {
                ChatManager.serverSendMessage(
                    $"no warps",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            foreach (var foundWarp in PSRMEssentials.Instance.WarpsDatabase.Warps)
            {
                ChatManager.serverSendMessage(
                    foundWarp.WarpName,
                    Color.cyan,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
            }
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "Warps";
        public string Help => "Gets all available warps";
        public string Syntax => "/warps";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "ps.essentials.warps.warps" };
    }
}