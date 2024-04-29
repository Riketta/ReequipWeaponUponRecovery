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

            HarmonyLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
            HarmonyLog.Log($"[{Prefix}] Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
#if DEBUG
            DebugLog.DumpStackTrace();
#endif

            if (GlobalState.CanSkipNextCallOfDropDropAllNearPawn)
            {
                GlobalState.CanSkipNextCallOfDropDropAllNearPawn = false;

                bool isColonist = pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer; // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?

                if (isColonist && GlobalState.ModSettings.KeepColonistsInventory && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfDeadColonists))
                {
                    GlobalState.Logger.Trace($"[{Prefix}] Preventing original method execution! Colonis will keep its inventory.");
                    return false;
                }

                if (!isColonist && GlobalState.ModSettings.KeepOtherPawnsInventory && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfOtherDeadPawns))
                {
                    GlobalState.Logger.Trace($"[{Prefix}] Preventing original method execution! Non-player's pawn will keep its inventory.");
                    return false;
                }
            }

            return true;
        }
    }
}
