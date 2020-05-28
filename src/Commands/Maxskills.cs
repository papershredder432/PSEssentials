using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    class Maxskills : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "maxskills";

        public string Help => "Maxes out your skills.";

        public string Syntax => "/maxskills";

        public List<string> Aliases => new List<string>() { "maxs" };

        public List<string> Permissions => new List<string>() { "ps.essentials.maxskills" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var PLAYER = caller as UnturnedPlayer;

            PLAYER.MaxSkills();
            UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("skills_maxed"));
        }
    }
}
