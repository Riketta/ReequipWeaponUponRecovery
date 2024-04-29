using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ReequipWeaponUponRecovery.Patches
{
    /// <summary>
    /// public void DropAllNearPawn(IntVec3 pos, bool forbid = false, bool unforbid = false).
    /// </summary>
    [HarmonyPatch(typeof(Pawn_InventoryTracker), "DropAllNearPawn")]
    internal class DropAllNearPawn_Patches
    {
        private const string Prefix = "Pawn_InventoryTracker.DropAllNearPawn";

        [HarmonyPrefix]
        public static bool DropAllNearPawn(Pawn_InventoryTracker __instance)
        {
            Pawn pawn = __instance.pawn;
            if (pawn is null)
                return true;

            DebugLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
            DebugLog.Log($"[{Prefix}] Caller: {DebugLog.GetCallingClassAndMethodNames()}.");
#if DEBUG
            DebugLog.DumpStackTrace();
#endif

            if (GlobalState.CanSkipNextCallOfDropDropAllNearPawn)
            {
                GlobalState.CanSkipNextCallOfDropDropAllNearPawn = false;

                // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?
                if ((GlobalState.Config.KeepOthersPawnInventory || (GlobalState.Config.KeepOthersPawnInventory && pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer)) &&
                    (!pawn.Dead || GlobalState.Config.KeepWeaponAndInventoryForDeadPawn))
                {
                    DebugLog.Log($"[{Prefix}] Preventing original method execution!");
                    return false;
                }
            }

            return true;
        }
    }
}
