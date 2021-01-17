using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.Warps
{
    public class Warp : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;
            
            var warpName = command[0];

            if (!PSRMEssentials.Instance.WarpsDatabase.Warps.Exists(x => x.WarpName == warpName))
            {
                ChatManager.serverSendMessage(
                    $"warp no exists",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            var warp = PSRMEssentials.Instance.WarpsDatabase.Warps.Single(x => x.WarpName == warpName);
            
            unturnedPlayer.Player.teleportToLocation(new Vector3(warp.PositionX, warp.PositionY, warp.PositionZ), 0);
            ChatManager.serverSendMessage(
                $"teleported you to {warpName}",
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "Warp";
        public string Help => "Teleports you to a warp.";
        public string Syntax => "/warp <Warp>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "ps.essentials.warps.warp" };
    }
}