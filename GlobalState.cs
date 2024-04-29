using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReequipWeaponUponRecovery
{
    internal class GlobalState
    {
        public static ModSettings ModSettings { get; set; }

        /// <summary>
        /// Mark that next DropAllEquipment call can be prevented if necessary.
        /// 
        /// Valid call chains to prevent execution:
        /// "Pawn.DropAndForbidEverything -> Pawn_EquipmentTracker.DropAllEquipment";
        /// "Pawn_EquipmentTracker.Notify_PawnSpawned -> Pawn_EquipmentTracker.DropAllEquipment".
        /// 
        /// Not valid (should be executed):
        /// "Pawn.Strip -> Pawn_EquipmentTracker.DropAllEquipment".
        /// </summary>
        public static bool CanSkipNextCallOfDropAllEquipment { get; set;}

        /// <summary>
        /// Mark that next DropDropAllNearPawn call can be prevented if necessary.
        /// 
        /// Valid call chains to prevent execution:
        /// "Pawn.DropAndForbidEverything -> Pawn_InventoryTracker.DropAllNearPawn";
        /// 
        /// Not valid (should be executed):
        /// "Pawn.Strip -> Pawn_InventoryTracker.DropAllNearPawn";
        /// "CaravanEnterMapUtility.DropAllInventory -> Pawn_InventoryTracker.DropAllNearPawn".
        /// </summary>
        public static bool CanSkipNextCallOfDropDropAllNearPawn { get; set;}
    }
}
