using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;

namespace papershredder432.PSEssentials.Handlers
{
    public class PSPlayerDeath
    {
        public void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            if (PSEssentials.Instance.Configuration.Instance.deathMessagesConfig.enabled)
            {
                ChatManager.serverSendMessage($"{player.DisplayName} was killed by {cause} in the {limb} from {murderer}",
                    UnityEngine.Color.red,
                    null,
                    null,
                    EChatMode.SAY,
                    PSEssentials.Instance.Configuration.Instance.deathMessagesConfig.img,
                    false);
            }
            else if (!PSEssentials.Instance.Configuration.Instance.deathMessagesConfig.enabled) return;

            PSEssentials.Instance.deathList[player.SteamName] = new Deaths() { deathPos = player.Position, lastDeath = DateTime.Now };
        }
    }
}
