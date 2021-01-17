using System.Collections.Generic;
using Elib;
using PSRMEssentials.Database;
using PSRMEssentials.Services;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials
{
    public class PSRMEssentials : RocketPlugin<PSRMEssentialsConfiguration>
    {
        public static PSRMEssentials Instance { get; set; }
        public Loads Loader = new Loads();
        
        public WarpsDatabase WarpsDatabase { get; private set; }
        public WarpsService WarpsService { get; private set; }
        

        protected override void Load()
        {
            Instance = this;
            Loader.Loaded(Name, Assembly.GetName().Version);

            StartCoroutine(Broadcast());
            
            WarpsDatabase = new WarpsDatabase();
            WarpsDatabase.Reload();
            WarpsService = gameObject.AddComponent<WarpsService>();
        }

        protected override void Unload()
        {
            Instance = null;
            Loader.Unloaded(Name, Assembly.GetName().Version);
            
            StopCoroutine(Broadcast());
            
            Destroy(WarpsService);
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
        };

        #region AutoBroadcast
        private int _autobroadcastindex = 0;
        private IEnumerator<WaitForSeconds> Broadcast()
        {
            while (true)
            {
                BroadcastMessage();
                yield return new WaitForSeconds(Configuration.Instance.AutoBroadcastConfig.Delay);
            }
        }

        private void BroadcastMessage()
        {
            if (_autobroadcastindex >= Configuration.Instance.AutoBroadcastConfig.AutoBroadcasts.Count)
                _autobroadcastindex = 0;

            var currentMessage = Configuration.Instance.AutoBroadcastConfig.AutoBroadcasts[_autobroadcastindex];
            
            ChatManager.serverSendMessage(
                currentMessage.Message.Replace("{", "<").Replace("}", ">"),
                Color.yellow,
                null,
                null,
                EChatMode.GLOBAL,
                currentMessage.ImageUrl,
                true);

            _autobroadcastindex++;
        }
        #endregion
    }
}