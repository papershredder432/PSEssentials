using System.Linq;
using PSRMEssentials.Database;
using PSRMEssentials.Models;
using UnityEngine;

namespace PSRMEssentials.Services
{
    public class WarpsService : MonoBehaviour
    {
        private WarpsDatabase WarpsDatabase => PSRMEssentials.Instance.WarpsDatabase;

        void Awake()
        {
            
        }

        void Start()
        {
            
        }

        private void OnDestroy()
        {
            
        }

        public void RegisterWarp(string warpName, Vector3 warpPosition)
        {
            var warp = new Warp
            {
                WarpName = warpName,
                PositionX = warpPosition.x,
                PositionY = warpPosition.y,
                PositionZ = warpPosition.z
            };
            
            WarpsDatabase.CreateWarp(warp);
        }

        public void RemoveWarp(string warpName)
        {
            var warp = WarpsDatabase.Warps.Single(x => x.WarpName == warpName);
            WarpsDatabase.DeleteWarp(warp);
        }

        public void RenameWarp(string oldWarpName, string newWarpName)
        {
            WarpsDatabase.RenameWarp(oldWarpName, newWarpName);
        }
    }
}