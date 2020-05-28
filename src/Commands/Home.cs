using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace papershredder432.PSEssentials.Commands
{
    class Home : IRocketCommand
    {
        public Vector3 bedPos;

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "home";

        public string Help => "Teleports you to your claimed bed.";

        public string Syntax => "/home";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "ps.essentials.home" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var SID = caller as UnturnedPlayer;
            byte bedRot;

            if (PSEssentials.Instance.Configuration.Instance.homeConfig.enabled)
            {
                if (!BarricadeManager.tryGetBed(SID.CSteamID, out bedPos, out bedRot))
                {
                    UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("no_home"));
                    return;
                } else
                {
                    if (SID.Stance == EPlayerStance.DRIVING || SID.Stance == EPlayerStance.SITTING)
                    {
                        UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("driving"));
                        return;
                    } else
                    {
                        bedPos = new Vector3(bedPos.x, bedPos.y + 0.5f, bedPos.z);

                        SID.Player.teleportToLocation(bedPos, bedRot);
                        UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("home_found"));
                    }
                }
            } else
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("disabled"), "Home"));
            }

            
        }
    }
}
