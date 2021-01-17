using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.PlayerManager
{
    public class MaxSkills : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;
            
            unturnedPlayer.MaxSkills();
            ChatManager.serverSendMessage(
                $"maxed skills",
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "Maxskills";
        public string Help => "Max out your skills";
        public string Syntax => "/maxskills";
        public List<string> Aliases => new List<string> { "max" };
        public List<string> Permissions => new List<string> { "ps.essentials.playermanager.maxskills" };
    }
}