using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace papershredder432.PSEssentials.Handlers
{
    public class PSPlayerDisconnected
    {
        public void OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enabled)
            {
                ChatManager.serverSendMessage(string.Format(PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.disconnectMessage, player.SteamName),
                    UnturnedChat.GetColorFromName(PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.connectColor, UnityEngine.Color.yellow),
                    null,
                    null,
                    EChatMode.SAY,
                    PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.disconnectImg,
                    false);
            }
            else if (!PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enabled) return;
        }
    }
}
