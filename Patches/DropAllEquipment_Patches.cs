using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ReequipWeaponUponRecovery.Patches
{
    /// <summary>
    /// public void DropAllEquipment(IntVec3 pos, bool forbid = true, bool rememberPrimary = false)
    /// </summary>
    [HarmonyPatch(typeof(Pawn_EquipmentTracker), "DropAllEquipment")]
    internal class DropAllEquipment_Patches
    {
        private const string Prefix = "Pawn_EquipmentTracker.DropAllEquipment";

#if DEBUG
        [HarmonyPrefix]
        public static bool DropAllEquipment(Pawn_EquipmentTracker __instance, bool forbid, bool rememberPrimary)
        {
            DebugLog.Log($"[{Prefix}] Forbid: {forbid}; Remember Primary: {rememberPrimary}.");
            DebugLog.Log($"[{Prefix}] Caller: {DebugLog.GetCallingClassAndMethodNames()}.");

            return true;
        }
#endif
    }
}
