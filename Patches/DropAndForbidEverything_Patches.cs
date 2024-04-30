using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static HarmonyLib.Code;

namespace ReequipWeaponUponRecovery.Patches
{
    /// <summary>
    /// public void DropAndForbidEverything(bool keepInventoryAndEquipmentIfInBed = false, bool rememberPrimary = false).
    /// Caller of Pawn_EquipmentTracker.DropAllEquipment and Pawn_InventoryTracker.DropAllNearPawn.
    /// </summary>
    [HarmonyPatch(typeof(Pawn), "DropAndForbidEverything")]
    internal class DropAndForbidEverything_Patch
    {
        private const string Prefix = "Pawn.DropAndForbidEverything";

        [HarmonyPrefix]
        public static bool DropAndForbidEverythingPrefix(Pawn __instance, ref bool keepInventoryAndEquipmentIfInBed, ref bool rememberPrimary)
        {
            var pawn = __instance;
            if (pawn is null)
                return true;

            HarmonyLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Primary: {pawn.equipment?.Primary?.ToStringSafe()}; PrimaryEq: {pawn.equipment?.PrimaryEq?.ToStringSafe()}; Spawned: {pawn.Spawned}; {keepInventoryAndEquipmentIfInBed}; {rememberPrimary}.");

            GlobalState.CanSkipNextCallOfDropAllEquipment = true;
            GlobalState.CanSkipNextCallOfDropDropAllNearPawn = true;

            return true;
        }

        [HarmonyPostfix]
        public static void DropAndForbidEverythingPostfix()
        {
            GlobalState.CanSkipNextCallOfDropAllEquipment = false;
            GlobalState.CanSkipNextCallOfDropDropAllNearPawn = false;
        }
    }
}
