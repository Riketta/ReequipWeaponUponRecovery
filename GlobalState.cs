﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReequipWeaponUponRecovery
{
    internal class GlobalState
    {
        public static Config Config { get; set; } = new Config();

        public static bool SkipNextCallOfDropAllEquipment { get; set;}
        public static bool SkipNextCallOfDropDropAllNearPawn { get; set;}
    }
}
