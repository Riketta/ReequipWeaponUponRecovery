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

            try
            {
            DebugLog.Log($"[{Prefix}] Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
#if DEBUG
            HarmonyLog.DumpStackTrace();
#endif

            if (GlobalState.CanSkipNextCallOfDropDropAllNearPawn)
            {
                GlobalState.CanSkipNextCallOfDropDropAllNearPawn = false;

                bool isColonist = pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer; // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?

                if (isColonist && GlobalState.ModSettings.KeepColonistsInventory && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfDeadColonists))
                {
                    DebugLog.Log($"[{Prefix}] [-] Preventing original method execution! Colonis (\"{pawn.Name?.ToStringShort}\") will keep its inventory.");
                    return false;
                }
                else if (!isColonist && GlobalState.ModSettings.KeepOtherPawnsInventory && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfOtherDeadPawns))
                {
                    DebugLog.Log($"[{Prefix}] [-] Preventing original method execution! Non-player's pawn (\"{pawn.Name?.ToStringShort}\") will keep its inventory.");
                    return false;
                }
                else
                    DebugLog.Log($"[{Prefix}] [+] Original method will be executed. Pawn (\"{pawn.Name?.ToStringShort}\") will drop its inventory items.");
            }
            else
                DebugLog.Log($"[{Prefix}] [X] Original undisturbed method will be executed. Pawn: \"{pawn.Name?.ToStringShort}\".");
            }
            catch (Exception ex)
            {
                DebugLog.Log($"[{Prefix}] Exception: {ex}.");
            }

            return true;
        }
    }
}
