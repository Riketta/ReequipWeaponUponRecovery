using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReequipWeaponUponRecovery
{
    internal class GlobalState
    {
        // TODO: move to customizable settings.
        public const bool KeepPlayersPawnWeapon = true;
        public const bool KeepPlayersPawnInventory = true;

        /// <summary>
        /// Keep weapons in inventory for non-player's pawns. For example: enemy human raids - OP for tribals.
        /// </summary>
        public const bool KeepOthersPawnWeapon = true;

        /// <summary>
        /// Keep items (non-weapons & equipment) in inventory for non-player's pawns.
        /// </summary>
        public const bool KeepOthersPawnInventory = true;

        /// <summary>
        /// Keep weapons and inventory for dead pawns or not and drop everything on death.
        /// </summary>
        public const bool KeepWeaponAndInventoryForDeadPawn = true;
    }
}
