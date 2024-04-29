using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ReequipWeaponUponRecovery.Patches
{
    /// <summary>
    /// public void DropAllEquipment(IntVec3 pos, bool forbid = true, bool rememberPrimary = false).
    /// </summary>
    [HarmonyPatch(typeof(Pawn_EquipmentTracker), "DropAllEquipment")]
    internal class DropAllEquipment_Patches
    {
        private const string Prefix = "Pawn_EquipmentTracker.DropAllEquipment";

        [HarmonyPrefix]
        public static bool DropAllEquipment(Pawn_EquipmentTracker __instance, bool forbid, bool rememberPrimary)
        {
            Pawn pawn = __instance.pawn;
            if (pawn is null)
                return true;

            HarmonyLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
            HarmonyLog.Log($"[{Prefix}] Forbid: {forbid}; Remember Primary: {rememberPrimary}.");
            HarmonyLog.Log($"[{Prefix}] Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
#if DEBUG
            DebugLog.DumpStackTrace();
#endif

            if (GlobalState.CanSkipNextCallOfDropAllEquipment)
            {
                GlobalState.CanSkipNextCallOfDropAllEquipment = false;

                bool isColonist = pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer; // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?

                if (isColonist && GlobalState.ModSettings.KeepColonistsWeapons && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfDeadColonists))
                {
                    GlobalState.Logger.Trace($"[{Prefix}] Preventing original method execution! Colonis will keep its weapon.");
                    return false;
                }

                if (!isColonist && GlobalState.ModSettings.KeepOtherPawnsWeapons && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfOtherDeadPawns))
                {
                    GlobalState.Logger.Trace($"[{Prefix}] Preventing original method execution! Non-player's pawn will keep its weapon.");
                    return false;
                }
            }

            return true;
        }
    }
}
