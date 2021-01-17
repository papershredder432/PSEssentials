using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.Warps
{
    public class RenameWarp : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;
            
            var oldWarpName = command[0];
            var newWarpName = command[1];

            if (!PSRMEssentials.Instance.WarpsDatabase.Warps.Exists(x => x.WarpName == oldWarpName))
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

            PSRMEssentials.Instance.WarpsService.RenameWarp(oldWarpName, newWarpName);
            ChatManager.serverSendMessage(
                $"renamed {oldWarpName} to {newWarpName}",
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "RenameWarp";
        public string Help => "Renames a warp.";
        public string Syntax => "/renamewarp <Warp> <NewName>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "ps.essentials.warps.renamewarp" };
    }
}