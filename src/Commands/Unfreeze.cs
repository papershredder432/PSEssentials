using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    class Unfreeze : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "unfreeze";

        public string Help => "Unfreezes a player.";

        public string Syntax => "/unfreeze <PlayerName>";

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions => new List<string>() { "ps.essentials.unfreeze" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "Unfreeze"));
                return;
            }

            UnturnedPlayer selectedPlayer = UnturnedPlayer.FromName(command[0]);

            if (selectedPlayer.Player.movement.totalSpeedMultiplier == 1) {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("not_frozen"), selectedPlayer.SteamName));
                return;
            } else if (selectedPlayer.Player.movement.totalSpeedMultiplier == 0)
            {
                selectedPlayer.Player.movement.sendPluginSpeedMultiplier(1);
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("caller_unfrozen"), selectedPlayer.SteamName));
                UnturnedChat.Say(selectedPlayer, PSEssentials.Instance.Translations.Instance.Translate(string.Format("player_unfrozen"), selectedPlayer.SteamName));
            }
        }
    }
}
