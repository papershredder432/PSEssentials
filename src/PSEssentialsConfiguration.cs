using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace papershredder432.PSEssentials
{
    public class PSEssentialsConfiguration : IRocketPluginConfiguration
    {
        public ConnectionMessagesConfig connectionMessagesConfig = new ConnectionMessagesConfig();
        public DeathMessagesConfig deathMessagesConfig = new DeathMessagesConfig();
        public AutoBroadcastConfig autoBroadcastConfig = new AutoBroadcastConfig();
        public WebCommandsConfig webCommandsConfig = new WebCommandsConfig();
        public WarpsConfig warpsConfig = new WarpsConfig();
        public HomeConfig homeConfig = new HomeConfig();
        public BackConfig backConfig = new BackConfig();

        public class ConnectionMessagesConfig
        {
            public bool enabled;
            public bool enableWebhooks;
            public string webhookUrl;
            public string connectMessage;
            public string disconnectMessage;
            public string connectColor;
            public string disconnectColor;
            public string connectImg;
            public string disconnectImg;
        }

        public class DeathMessagesConfig
        {
            public bool enabled;
            public bool enableWebhooks;
            public string webhookUrl;
            public string color;
            public string img;
        }

        public class AutoBroadcastConfig
        {
            public bool enabled;
            public int interval;

            [XmlArrayItem("Broadcast")]
            [XmlArray(ElementName = "Broadcast")]
            public Broadcast[] Broadcasts;
        }

        public class WebCommandsConfig
        {
            public bool enabled;
            [XmlArrayItem(ElementName = "WebCommand")]
            public List<WebCommand> WebCommands; 
            
        }

        public class WarpsConfig
        {
            public bool enabled;

            public List<Warp> Warps;
        }

        public class HomeConfig
        {
            public bool enabled;
        }

        public class BackConfig
        {
            public bool enabled;
        }

        public void LoadDefaults()
        {
            #region connectionMessagesConfig
            connectionMessagesConfig.enabled = true;
            connectionMessagesConfig.enableWebhooks = false;
            connectionMessagesConfig.webhookUrl = "";
            connectionMessagesConfig.connectMessage = "{0} has joined the server.";
            connectionMessagesConfig.disconnectMessage = "{0} has left the server.";
            connectionMessagesConfig.connectColor = "yellow";
            connectionMessagesConfig.disconnectColor = "yellow";
            connectionMessagesConfig.connectImg = "";
            connectionMessagesConfig.disconnectImg = "";
            #endregion

            #region deathMessagesConfig
            deathMessagesConfig.enabled = true;
            deathMessagesConfig.enableWebhooks = false;
            deathMessagesConfig.webhookUrl = "";
            deathMessagesConfig.color = "yellow";
            deathMessagesConfig.img = "";
            #endregion

            #region autoBroadcastConfig
            autoBroadcastConfig.enabled = true;
            autoBroadcastConfig.interval = 180;
            autoBroadcastConfig.Broadcasts = new Broadcast[]
            {
                new Broadcast("Welcome to our server network, enjoy your gaming!", "Green", ""),
                new Broadcast("Join our Discord at: https://discord.gg/ydjYVJ2", "Green", "https://www.freepnglogos.com/uploads/discord-logo-png/discord-logo-logodownload-download-logotipos-1.png"),
                new Broadcast("Please be polite to others.", "Green", "")

            };
            #endregion

            #region webCommandsConfig
            webCommandsConfig.enabled = true;
            webCommandsConfig.WebCommands = new List<WebCommand>
            {
                new WebCommand { Command = "discord", Description = "Join our Discord here!", Url = "https://discord.gg/ydjYVJ2", Help = "Requests you to our Discord." }
            };
            #endregion

            #region warpsConfig
            warpsConfig.enabled = true;
            warpsConfig.Warps = new List<Warp>
            {
                new Warp { Name = "Warp", X = 0, Y = 0, Z = 0 }
            };
            #endregion

            #region homeConfig
            homeConfig.enabled = true;
            #endregion

            #region
            backConfig.enabled = true;
            #endregion
        }

        public sealed class Broadcast
        {
            [XmlAttribute("Text")]
            public string Text;

            [XmlAttribute("Color")]
            public string Color;

            [XmlAttribute("Img")]
            public string Img;

            public Broadcast(string text, string color, string img)
            {
                Text = text;
                Color = color;
                Img = img;
            }
            public Broadcast()
            {
                Text = "";
                Color = "";
                Img = "";
            }
        }

        public class WebCommand
        {
            public string Url;
            public string Description;
            public string Command;
            public string Help;
        }

        [XmlRoot("warpsConfig")]
        public class Warp
        {
            [XmlAttribute] public string Name { get; set; }

            [XmlAttribute] public float X { get; set; }

            [XmlAttribute] public float Y { get; set; }

            [XmlAttribute] public float Z { get; set; }
        }
    }
}