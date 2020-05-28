using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace papershredder432.PSEssentials.Commands
{
    class Freeze : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "freeze";

        public string Help => "Freezes a player.";

        public string Syntax => "/freeze <PlayerName>";

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions => new List<string>() { "ps.essentials.freeze" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "Freeze"));
                return;
            }

            UnturnedPlayer selectedPlayer = UnturnedPlayer.FromName(command[0]);

            if (selectedPlayer.Player.movement.totalSpeedMultiplier == 1) {
                selectedPlayer.Player.movement.sendPluginSpeedMultiplier(0);
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("caller_frozen"), selectedPlayer.SteamName));
                UnturnedChat.Say(selectedPlayer, PSEssentials.Instance.Translations.Instance.Translate(string.Format("player_frozen"), selectedPlayer.SteamName));
            } else if (selectedPlayer.Player.movement.totalSpeedMultiplier == 0)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("already_frozen"), selectedPlayer.SteamName));
                return;
            }
        }
    }
}
