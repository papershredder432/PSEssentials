using Rocket.API;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    class WarpRename : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "warprename";

        public string Help => "Renames a warp.";

        public string Syntax => "/warprename <Name> <New Name>";

        public List<string> Aliases => new List<string>() { "warpren" };

        public List<string> Permissions => new List<string>() { "ps.essentials.warp.rename", "ps.essentials.warp.*" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var warps = PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps;
            var FindMe = warps.Find(x => x.Name.ToLower() == command[0].ToLower());

            if (command[0].Length < 1)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "Warprename"));
                return;
            } else if (!warps.Exists(x => x.Name == command[0]))
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("warp_!exists"), command[0]));
                return;
            } else
            {
                var oldName = FindMe.Name;
                if (command[1].Length < 1)
                {
                    UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("warp_!newname"));
                    return;
                }
                else
                {
                    FindMe.Name = command[1];
                    PSEssentials.Instance.Configuration.Save();
                    UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("warp_newname"), oldName, command[0]));
                }
            }
        }
    }
}
