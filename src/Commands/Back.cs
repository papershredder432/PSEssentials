using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;

namespace papershredder432.PSEssentials.Commands
{
    class Back : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "back";

        public string Help => "Teleports you back to your last death location.";

        public string Syntax => "/back";

        public List<string> Aliases => new List<string> { };

        public List<string> Permissions => new List<string>() { "ps.essentials.back" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var PLAYER = caller as UnturnedPlayer;

            if (!PSEssentials.Instance.Configuration.Instance.backConfig.enabled)
            {
                UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate(string.Format("disabled"), "Back"));
                return;
            } else
            {
                if (!PSEssentials.Instance.deathList.ContainsKey(PLAYER.SteamName))
                {
                    UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("back_nodeath"));
                    return;
                } else
                {
                    try
                    {
                        PLAYER.Player.teleportToLocation(PSEssentials.Instance.deathList[PLAYER.SteamName].deathPos, 0);
                        UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("back_success"));
                    } catch (Exception e)
                    {
                        UnturnedChat.Say(caller, PSEssentials.Instance.Translations.Instance.Translate("back_failed"));
                        return;
                    }
                }
            }
            
        }
    }
}
