using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace PSRMEssentials.Commands.Teleports
{
    public class TeleportWaypoint : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var unturnedPlayer = (UnturnedPlayer) caller;

            if (!unturnedPlayer.Player.quests.isMarkerPlaced)
            {
                ChatManager.serverSendMessage(
                    $"no waypoint",
                    Color.yellow,
                    null,
                    unturnedPlayer.SteamPlayer(),
                    EChatMode.SAY,
                    null,
                    true);
                return;
            }

            unturnedPlayer.Player.teleportToLocation(Ground(unturnedPlayer.Position).Value, 0);
            ChatManager.serverSendMessage(
                $"teleported to waypoint",
                Color.cyan,
                null,
                unturnedPlayer.SteamPlayer(),
                EChatMode.SAY,
                null,
                true);
        }

        public Vector3? Ground(Vector3 position)
        {
            int layerMasks = RayMasks.BARRICADE | RayMasks.BARRICADE_INTERACT | RayMasks.ENEMY | RayMasks.ENTITY | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.GROUND2 | RayMasks.ITEM | RayMasks.RESOURCE | RayMasks.STRUCTURE | RayMasks.STRUCTURE_INTERACT;

            if (Physics.Raycast(new Vector3(position.x, position.y, position.z), Vector3.down, out RaycastHit hit, 500,
                layerMasks))
            {
                return hit.point;
            }
            else
            {
                return null;
            }
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "TeleportWaypoint";
        public string Help => "Teleport to your waypoint.";
        public string Syntax => "/teleportwaypoint";
        public List<string> Aliases => new List<string> { "tpwp", "teleportwp" };
        public List<string> Permissions => new List<string> { "ps.essentials.teleports.teleportwaypoint" };
    }
}