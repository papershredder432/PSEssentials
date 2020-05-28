using Rocket.API;
using Rocket.Unturned.Chat;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    class WarpRemove : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "warpremove";

        public string Help => "Removes a warp location.";

        public string Syntax => "/warpremove <Name>";

        public List<string> Aliases => new List<string>() { "warprem" };

        public List<string> Permissions => new List<string>() { "ps.essentials.warp.remove", "ps.essentials.warp.*" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var warps = PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps;
            var FindMe = warps.Find(x => x.Name.ToLower() == command[0].ToLower());

            if (command[0].Length < 1)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "Warpremove"));
                return;
            }
            else if (!warps.Exists(x => x.Name == command[0]))
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("warp_!exists"));
                return;
            } else
            {
                warps.Remove(new PSEssentialsConfiguration.Warp() { Name = command[0] });
                PSEssentials.Instance.Configuration.Save();
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("warp_removed"), command[0]));
            }
        }
    }
}
