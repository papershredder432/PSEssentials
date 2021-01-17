using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.Warps
{
    public class DeleteWarp : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;
            
            var warpName = command[0];

            if (!PSRMEssentials.Instance.WarpsDatabase.Warps.Exists(x => x.WarpName == warpName))
            {
                ChatManager.serverSendMessage(
                    $"warp no exist",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            PSRMEssentials.Instance.WarpsService.RemoveWarp(warpName);
            ChatManager.serverSendMessage(
                $"deleted warp {warpName}",
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "DeleteWarp";
        public string Help => "Deletes a warp.";
        public string Syntax => "/deletewarp <WarpName>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "ps.essentials.warps.deletewarp" };
    }
}