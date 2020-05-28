using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace papershredder432.PSEssentials.Commands
{
    class Warp : IRocketCommand
    {
        public Vector3 TPPos;

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "warp";

        public string Help => "Teleports you to a warp.";

        public string Syntax => "/warp <Name>";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "ps.essentials.warps.warp", "ps.essentials.warps.*" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var warps = PSEssentials.Instance.Configuration.Instance.warpsConfig.Warps;
            var FindMe = warps.Find(x => x.Name.ToLower() == command[0].ToLower());
            var PLAYER = caller as UnturnedPlayer;

            if (!PSEssentials.Instance.Configuration.Instance.warpsConfig.enabled)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("disabled"), "Warp"));
                return;
            } else if (PSEssentials.Instance.Configuration.Instance.warpsConfig.enabled) {
                if (command[0].Length < 1)
                {
                    UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "Warp"));
                    return;
                } else if (command[0].ToLower() == FindMe.Name.ToLower())
                {
                    if (PLAYER.Stance == SDG.Unturned.EPlayerStance.DRIVING || PLAYER.Stance == SDG.Unturned.EPlayerStance.SITTING)
                    {
                        UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("driving"));
                        return;
                    }
                    else
                    {
                        TPPos = new Vector3(FindMe.X, FindMe.Y, FindMe.Z);
                        PLAYER.Player.teleportToLocation(TPPos, 0);
                        UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("warp_success"), FindMe.Name));
                    }
                }
                else if (!warps.Exists(x => x.Name == command[0]))
                {
                    UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("warp_!exists"));
                    return;
                }
            }
        }
    }
}