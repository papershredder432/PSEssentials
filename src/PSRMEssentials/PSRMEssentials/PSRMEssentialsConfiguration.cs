using System.Collections.Generic;
using Rocket.API;
using PSRMEssentials.Models;

namespace PSRMEssentials
{
    public class PSRMEssentialsConfiguration : IRocketPluginConfiguration
    {
        #region HomeConfiguration
        public HomeConfiguration HomeConfig => new HomeConfiguration();
        public class HomeConfiguration
        {
            public int Cooldown;
            public bool CancelOnDamage;
        }
        #endregion
        
        #region AutoBroadcast
        public AutoBroadcastConfiguration AutoBroadcastConfig = new AutoBroadcastConfiguration();
        public class AutoBroadcastConfiguration
        {
            public int Delay;
            public List<AutoBroadcast> AutoBroadcasts = new List<AutoBroadcast>();
        }
        #endregion
        
        public void LoadDefaults()
        {
            #region HomeConfig
            HomeConfig.Cooldown = 60;
            HomeConfig.CancelOnDamage = false;
            #endregion

            #region AutoBroadcastConfig
            AutoBroadcastConfig.Delay = 180;
            AutoBroadcastConfig.AutoBroadcasts = new List<AutoBroadcast>
            {
                new AutoBroadcast()
                {
                    Message = "{color=#ff00ff}Your message{/color}",
                    ImageUrl = "ImageUrl"
                }
            };
            #endregion
        }
    }
}