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
    public class PSPlayerConnected
    {
        public void OnPlayerConnected(UnturnedPlayer player)
        {
            if (PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enabled)
            {
                ChatManager.serverSendMessage(string.Format(PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.connectMessage, player.SteamName),
                    UnturnedChat.GetColorFromName(PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.connectColor, UnityEngine.Color.yellow),
                    null,
                    null,
                    EChatMode.SAY,
                    PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.connectImg,
                    false);
                if (PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enableWebhooks)
                {
                    WebhookManager.Webhook wb = new WebhookManager.Webhook();
                    //wb.SendEmbed(embed, "Player Connected", Instance.Configuration.Instance.connectionMessagesConfig.webhookUrl);
                }
                else if (!PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enableWebhooks) return;

            }
            else if (!PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enabled)
            {
                if (PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enableWebhooks)
                {
                    WebhookManager.Webhook wb = new WebhookManager.Webhook();
                    //wb.SendEmbed(embed, "Player Connected", Instance.Configuration.Instance.connectionMessagesConfig.webhookUrl);
                }
                else if (!PSEssentials.Instance.Configuration.Instance.connectionMessagesConfig.enableWebhooks) return;
            }
        }
    }
}
