using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.PlayerManager
{
    public class Experience : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;
            
            if (!uint.TryParse(command[0], out uint amount))
            {
                ChatManager.serverSendMessage(
                    $"not valid".Replace("<", "{").Replace(">", "}"),
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            unturnedPlayer.Experience = +amount;
            ChatManager.serverSendMessage(
                $"<b>{amount}</b> was given".Replace("<", "{").Replace(">", "}"),
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "Experience";
        public string Help => "Give yourself experience";
        public string Syntax => "/experience <amount>";
        public List<string> Aliases => new List<string> { "xp", "exp" };
        public List<string> Permissions => new List<string> { "ps.essentials.playermanager.experience" };
    }
}