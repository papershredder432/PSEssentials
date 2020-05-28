using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace papershredder432.PSEssentials.Commands
{
    class Uncuff : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "uncuff";

        public string Help => "Force uncuffs a player.";

        public string Syntax => "/uncuff <Player>";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "ps.essentials.uncuff" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, "Invalid use of Uncuff.");
                return;
            }

            UnturnedPlayer selectedPlayer = UnturnedPlayer.FromName(command[0]);

            try
            {
                selectedPlayer.Player.stance.checkStance(EPlayerStance.STAND, true);
                selectedPlayer.Player.animator.sendGesture(EPlayerGesture.ARREST_STOP, true);
                UnturnedChat.Say(caller, $"Successfully uncuffed {selectedPlayer.Player.name}.");
            } catch (Exception e)
            {
                UnturnedChat.Say(caller, $"Unable to uccuff {selectedPlayer.Player.name}");
                return;
            }
        }
    }
}
