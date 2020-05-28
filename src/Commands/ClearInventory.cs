using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    class ClearInventory : IRocketCommand
    {
        private UnturnedPlayer player;

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "clearinventory";

        public string Help => "Clears yours or another player's inventory.";

        public string Syntax => "/clearinventory [PlayerName]";

        public List<string> Aliases => new List<string>() { "ci" };

        public List<string> Permissions => new List<string>() { "ps.essentials.ci" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer playerId = (UnturnedPlayer)caller;
            if (command.Length <= 1)
            {
                UnturnedChat.Say(playerId, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "ClearInventory"));
                return;
            }
            if (command.Length >= 1)
            {
                player = UnturnedPlayer.FromName(command[0]);
            }

            bool done = ClearInv(player);
            if (command.Length == 2)
            {
                if (command[1].ToLower() != "true")
                {
                    UnturnedChat.Say(playerId, PSEssentials.Instance.Translations.Instance.Translate(string.Format("invalid_use"), "ClearInventory"));
                    return;
                }

                done = ClearClothes(player);
            }

            if (!done)
            {
                UnturnedChat.Say(playerId, PSEssentials.Instance.Translations.Instance.Translate(string.Format("command_failed"), "ClearInventory"));
                return;
            } else
            {
                UnturnedChat.Say(playerId, PSEssentials.Instance.Translations.Instance.Translate(string.Format("command_failed"), player.SteamName));
            }
        }

        public bool ClearInv(UnturnedPlayer player)
        {
            bool returnv = false;
            try
            {
                player.Player.equipment.dequip();
                for (byte p = 0; p < (PlayerInventory.PAGES - 1); p++)
                {
                    byte itemc = player.Player.inventory.getItemCount(p);
                    if (itemc > 0)
                    {
                        for (byte p1 = 0; p1 < itemc; p1++)
                        {
                            player.Player.inventory.removeItem(p, 0);
                        }
                    }
                }
                player.Player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
                {
                    (byte)0,
                    (byte)0,
                    new byte[0]
                });
                player.Player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
                {
                    (byte)1,
                    (byte)0,
                    new byte[0]
                });
                returnv = true;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return returnv;
        }

        public bool ClearClothes(UnturnedPlayer player)
        {
            bool returnv = false;
            try
            {
                player.Player.clothing.askWearBackpack(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearGlasses(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearHat(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearMask(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearPants(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearShirt(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearVest(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                returnv = true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return returnv;
        }
    }
}
