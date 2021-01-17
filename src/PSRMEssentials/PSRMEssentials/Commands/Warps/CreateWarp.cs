using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace PSRMEssentials.Commands.Warps
{
    public class CreateWarp : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;

            var warpName = command[0];

            if (PSRMEssentials.Instance.WarpsDatabase.Warps.Exists(x => x.WarpName == warpName))
            {
                ChatManager.serverSendMessage(
                    $"warp already exists",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            PSRMEssentials.Instance.WarpsService.RegisterWarp(warpName, unturnedPlayer.Position);
            ChatManager.serverSendMessage(
                $"created warp {warpName}",
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "CreateWarp";
        public string Help => "Creates a warp at your current location.";
        public string Syntax => "/createwarp <WarpName>";
        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>
        {
            "ps.essentials.warps.createwarp"
        };
    }
}