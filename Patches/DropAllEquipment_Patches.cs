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

            DebugLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
            DebugLog.Log($"[{Prefix}] Forbid: {forbid}; Remember Primary: {rememberPrimary}.");
            DebugLog.Log($"[{Prefix}] Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
#if DEBUG
            HarmonyLog.DumpStackTrace();
#endif

            if (GlobalState.CanSkipNextCallOfDropAllEquipment)
            {
                GlobalState.CanSkipNextCallOfDropAllEquipment = false;

                bool isColonist = pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer; // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?

                if (isColonist && GlobalState.ModSettings.KeepColonistsWeapons && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfDeadColonists))
                {
                    DebugLog.Log($"[{Prefix}] Preventing original method execution! Colonis (\"{pawn.Name?.ToStringShort}\") will keep its weapon. Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
                    return false;
                }
                else if (!isColonist && GlobalState.ModSettings.KeepOtherPawnsWeapons && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfOtherDeadPawns))
                {
                    DebugLog.Log($"[{Prefix}] Preventing original method execution! Non-player's pawn (\"{pawn.Name?.ToStringShort}\") will keep its weapon. Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
                    return false;
                }
                else
                    DebugLog.Log($"[{Prefix}] Original method will be executed. Pawn (\"{pawn.Name?.ToStringShort}\") will drop its weapon. Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
            }
            else
                DebugLog.Log($"[{Prefix}] Original undisturbed method will be executed. Pawn: \"{pawn.Name?.ToStringShort}\". Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");

            return true;
        }
    }
}
