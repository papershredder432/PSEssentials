using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace PSRMEssentials.Commands.Teleports
{
    public class Home : IRocketCommand
    {
        public Dictionary<CSteamID, DateTime> coolDown = new Dictionary<CSteamID, DateTime>();
        
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;

            if (coolDown.ContainsKey(unturnedPlayer.CSteamID) || coolDown.FirstOrDefault(x=> x.Key == unturnedPlayer.CSteamID).Value > DateTime.Now)
            {
                var secondsLeft = coolDown.FirstOrDefault(x => x.Key == unturnedPlayer.CSteamID).Value - DateTime.Now;
                ChatManager.serverSendMessage(
                    $"<b>{secondsLeft}</b> seconds left".Replace("{", "<").Replace("}", ">"),
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            if (coolDown.FirstOrDefault(x => x.Key == unturnedPlayer.CSteamID).Value > DateTime.Now)
                coolDown.Remove(unturnedPlayer.CSteamID);

            if (!BarricadeManager.tryGetBed(unturnedPlayer.CSteamID, out _, out _))
            {
                ChatManager.serverSendMessage(
                    $"no bed",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            if (unturnedPlayer.Stance == EPlayerStance.DRIVING || unturnedPlayer.Stance == EPlayerStance.DRIVING)
            {
                ChatManager.serverSendMessage(
                    $"in vehicle",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            unturnedPlayer.Player.teleportToBed();
            ChatManager.serverSendMessage(
                $"teleported to bed",
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
            
            coolDown.Add(unturnedPlayer.CSteamID, DateTime.Now.AddSeconds(PSRMEssentials.Instance.Configuration.Instance.HomeConfig.Cooldown));
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "home";
        public string Help => "Teleports you to your claimed bed.";
        public string Syntax => "/home";
        public List<string> Aliases => new List<string>{};
        public List<string> Permissions => new List<string> { "ps.essentials.teleports.home" };
    }
}