using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PSRMEssentials.Models;

namespace PSRMEssentials.Database
{
    public class WarpsDatabase
    {
        private DataStorage<List<Warp>> WarpsStorage { get; set; }
        public List<Warp> Warps { get; private set; }

        public WarpsDatabase()
        {
            WarpsStorage = new DataStorage<List<Warp>>(PSRMEssentials.Instance.Directory, "Warps.json");
        }
        
        public void Reload()
        {
            Warps = WarpsStorage.Read();
            if (Warps == null)
            {
                Warps = new List<Warp>();
                WarpsStorage.Save(Warps);
            }
        }

        public void CreateWarp(Warp warp)
        {
            Warps.Add(warp);
            WarpsStorage.Save(Warps);
        }

        public void DeleteWarp(Warp warp)
        {
            Warps.Remove(warp);
            WarpsStorage.Save(Warps);
        }

        public void RenameWarp(string oldName, string newName)
        {
            var selectedWarp = Warps.Single(x => x.WarpName == oldName);
            selectedWarp.WarpName = newName;
            WarpsStorage.Save(Warps);
        }
    }
}