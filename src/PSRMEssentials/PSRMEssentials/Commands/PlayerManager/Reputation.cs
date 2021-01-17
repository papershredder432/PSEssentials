using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.PlayerManager
{
    public class Reputation : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;
            
            if (!int.TryParse(command[0], out int amount))
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

            unturnedPlayer.Reputation = +amount;
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
        public string Name => "Reputation";
        public string Help => "Give yourself reputation";
        public string Syntax => "/reputation <amount>";
        public List<string> Aliases => new List<string> { "rep" };
        public List<string> Permissions => new List<string> { "ps.essentials.playermanager.reputation" };
    }
}