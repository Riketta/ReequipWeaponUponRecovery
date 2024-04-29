using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReequipWeaponUponRecovery
{
    internal class GlobalState
    {
        public static Config Config { get; set; } = new Config();

        /// <summary>
        /// Mark that next DropAllEquipment call can be prevented if necessary.
        public static bool CanSkipNextCallOfDropAllEquipment { get; set;}
        public static bool CanSkipNextCallOfDropDropAllNearPawn { get; set;}
    }
}
