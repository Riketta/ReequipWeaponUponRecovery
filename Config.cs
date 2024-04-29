using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReequipWeaponUponRecovery
{
    internal class Config
    {
        // TODO: move to customizable settings.
        public bool KeepPlayersPawnWeapon { get; set; } = true;
        public bool KeepPlayersPawnInventory { get; set; } = true;

        /// <summary>
        /// Keep weapons in inventory for non-player's pawns. For example: enemy human raids - OP for tribals.
        /// </summary>
        public bool KeepOthersPawnWeapon { get; set; } = true;

        /// <summary>
        /// Keep items (non-weapons & equipment) in inventory for non-player's pawns.
        /// </summary>
        public bool KeepOthersPawnInventory { get; set; } = true;

        /// <summary>
        /// Keep weapons and inventory for dead pawns or not and drop everything on death.
        /// </summary>
        public bool KeepWeaponAndInventoryForDeadPawn { get; set; } = true;
    }
}
