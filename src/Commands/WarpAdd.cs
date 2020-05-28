using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    class WarpAdd : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "warpadd";

        public string Help => "Adds a warp location.";

        public string Syntax => "/warpadd <Name>";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "ps.essentials.warp.add", "ps.essentials.warp.*" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var warps = PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps;
            var FindMe = warps.Find(x => x.Name.ToLower() == command[0].ToLower());

            if (command[0].Length < 1)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "Warpadd"));
                return;
            }
            else if (warps.Exists(x => x.Name == command[0]))
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("warp_exists"), command[0]));
                return;
            }
            else
            {
                var PLAYER = caller as UnturnedPlayer;

                PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps.Add(new PSEssentialsConfiguration.Warp() { Name = command[0], X = PLAYER.Position.x, Y = PLAYER.Position.y, Z = PLAYER.Position.z });
                PSEssentials.Instance.Configuration.Save();
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("warp_added"), command[0]));
            }
        }
    }
}
