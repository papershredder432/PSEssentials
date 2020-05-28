using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;

namespace papershredder432.PSEssentials
{
    public class PSEssentials : RocketPlugin<PSEssentialsConfiguration>
    {
        public static PSEssentials Instance;

        public static Handlers.PSPlayerConnected PSPlayerConnected;
        public static Handlers.PSPlayerDisconnected PSPlayerDisconnected;
        public static Handlers.PSPlayerDeath PSPlayerDeath;

        public int lastIndex = 0;
        public DateTime? lastBroadcast = null;

        public IDictionary<string, Deaths> deathList;

        void FixedUpdate()
        {
            printBroadcast();
        }

        protected override void Load()
        {
            Instance = this;
            Logger.LogWarning("[PSEssentials] Loaded, made by papershredder432, join the support Discord here: https://discord.gg/ydjYVJ2");
            Logger.Log($"[PSEssentials] Successfully added {Instance.Configuration.Instance.webCommandsConfig.WebCommands.Count} WebCommands!");
            Logger.Log($"[PSEssentials] Successfully loaded {Instance.Configuration.Instance.warpsConfig.Warps.Count} Warps.");

            U.Events.OnPlayerConnected += PSPlayerConnected.OnPlayerConnected;
            U.Events.OnPlayerDisconnected += PSPlayerDisconnected.OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerDeath += PSPlayerDeath.OnPlayerDeath;

            deathList = new Dictionary<string, Deaths>();

            /* if (Configuration.Instance.webCommandsConfig.WebCommands.Count != 0)
            {
                foreach (var command in Instance.Configuration.Instance.webCommandsConfig.WebCommands)
                {
                    WebCommands webCommands = new WebCommands(command.Command, command.Description, command.Url, command.Help);
                    R.Commands.Register(webCommands);
                }
            } */
        }

        protected override void Unload()
        {
            Instance = null;
            Logger.LogWarning("[PSEssentials] Unloaded.");

            U.Events.OnPlayerConnected -= PSPlayerConnected.OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= PSPlayerDisconnected.OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerDeath -= PSPlayerDeath.OnPlayerDeath;

            /* foreach (var command in Instance.Configuration.Instance.webCommandsConfig.WebCommands)
            {
                R.Commands.DeregisterFromAssembly(Assembly);
            } */
        }

        public static void OpenUrl(UnturnedPlayer player, string url, string description)
        {
            player.Player.sendBrowserRequest(description, url);
        }

        private void printBroadcast()
        {
            try
            {
                if (State == PluginState.Loaded && Instance.Configuration.Instance.autoBroadcastConfig.Broadcasts != null && (lastBroadcast == null || ((DateTime.Now - lastBroadcast.Value).TotalSeconds > Instance.Configuration.Instance.autoBroadcastConfig.interval)))
                {
                    if (lastIndex > (Instance.Configuration.Instance.autoBroadcastConfig.Broadcasts.Length - 1)) lastIndex = 0;
                    PSEssentialsConfiguration.Broadcast broadcasts = Instance.Configuration.Instance.autoBroadcastConfig.Broadcasts[lastIndex];
                    ChatManager.serverSendMessage(broadcasts.Text, UnturnedChat.GetColorFromName(broadcasts.Color, UnityEngine.Color.green), null, null, EChatMode.SAY, broadcasts.Img, false);
                    Logger.Log(broadcasts.Text);
                    lastBroadcast = DateTime.Now;
                    lastIndex++;
                }
            } catch (Exception ex)
            {
                Logger.LogException(ex, "OOF.");
            }
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            #region Universal Commands
            { "invalid_use", "Invalid use of {0}." },
            { "command_failed", "{0} has failed to execute." },
            { "disabled", "{0} has been disabled." },
            { "driving", "You can not teleport you while driving." },
            #endregion

            #region ClearInventory
            { "inventory_cleared", "{0}'s inventory has been cleared." },
            #endregion

            #region Un/Cuff
            { "cuff_success", "Successfully cuffed {0}." },
            { "cuff_failed", "Unable to cuff {0}." },
            #endregion

            #region Un/Freeze
            { "caller_frozen", "{0} has been frozen." },
            { "caller_unfrozen", "{0} has been unfrozen." },
            { "player_frozen", "You have been frozen." },
            { "player_unfrozen", "You have been unfrozen." },
            { "already_frozen", "{0} is already frozen." },
            { "not_frozen", "{0} is not frozen" },
            #endregion

            #region Home
            { "no_home", "No bed was found." },
            { "home_found", "Successfully teleported you to your bed." },
            #endregion

            #region Warps
            { "warp_success", "Successfully teleported you to warp: {0}." },
            { "warp_failed", "Could not teleport you to warp location." },
            { "warp_added", "Added warp: {0}" },
            { "warp_!exists", "Warp: {0} does not exist." },
            { "warp_exists", "Warp {0} already exists." },
            { "warp_removed", "Removed warp; {0}" },
            { "warp_renamed", "Warp {0} has been renamed to {1}." },
            { "warp_!newname", "Specify a new name for the warp." },
            { "warp_newname", "Renamed warp from {0} to {1}" },
            { "warp_none", "There are no available warps." },
            #endregion

            #region Maxskills
            { "skills_maxed", "Your skills have been maxed." },
            #endregion

            #region Back
            { "back_success", "You have been teleported to your last death." },
            { "back_failed", "You could not be teleported to your last death." },
            { "back_nodeath", "You have not died yet." },
            #endregion
        };
    }
}