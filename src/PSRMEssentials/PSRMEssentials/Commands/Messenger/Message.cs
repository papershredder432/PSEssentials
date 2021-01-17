using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.Messenger
{
    public class Message : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;
            var targetPlayer = UnturnedPlayer.FromName(command[0]);
            
            if (UnturnedPlayer.FromName(command[0]) == null)
            {
                ChatManager.serverSendMessage(
                    "no player",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }
            
            ChatManager.serverSendMessage(
                $"(To) {targetPlayer.CharacterName} : {string.Join(" ", command).Replace(command[0], "")}",
                Color.white,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                unturnedPlayer.SteamProfile.AvatarIcon.ToString(),
                true);
            
            ChatManager.serverSendMessage(
                $"(From) {unturnedPlayer.CharacterName} : {string.Join(" ", command).Replace(command[0], "")}",
                Color.white,
                unturnedPlayer.SteamPlayer(),
                targetPlayer.SteamPlayer(),
                EChatMode.SAY,
                unturnedPlayer.SteamProfile.AvatarIcon.ToString(),
                true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "message";
        public string Help => "Message a player.";
        public string Syntax => "/message <Player> <Message>";
        public List<string> Aliases => new List<string> { "pm", "tell" };
        public List<string> Permissions => new List<string> { "ps.essentials.messenger.message" };
    }
}