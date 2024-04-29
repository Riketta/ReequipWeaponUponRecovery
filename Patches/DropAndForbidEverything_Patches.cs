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
    /// </summary>
    [HarmonyPatch(typeof(Pawn), "DropAndForbidEverything")]
    internal class DropAndForbidEverything_Patch
    {
        private const string Prefix = "Pawn.DropAndForbidEverything";

#if DEBUG
        [HarmonyPrefix]
        public static bool LogDropAndForbidEverything(Pawn __instance, ref bool keepInventoryAndEquipmentIfInBed, ref bool rememberPrimary)
        {
            var pawn = __instance;
            if (pawn is null)
                return true;

            DebugLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Primary: {pawn.equipment?.Primary?.ToStringSafe()}; PrimaryEq: {pawn.equipment?.PrimaryEq?.ToStringSafe()}; Spawned: {pawn.Spawned}; {keepInventoryAndEquipmentIfInBed}; {rememberPrimary}.");

            return true;
        }
#endif
    }
}
