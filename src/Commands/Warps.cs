using Rocket.API;
using Rocket.Unturned.Chat;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    class Warps : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "warps";

        public string Help => "Shows a list of all available warps.";

        public string Syntax => "/warps";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "ps.essentials.warps.warps", "ps.essentials.warps.*" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var warps = PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps;
            var FindMe = warps.Find(x => x.Name.ToLower() == command[0].ToLower());

            if (PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps.Count == 0)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("warp_none"));
                return;
            }
            else
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps.ToString());
            }
        }
    }
}
