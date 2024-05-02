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

            try
            {
                string pawnName = pawn.Name is null ? pawn.ThingID : pawn.Name.ToStringShort;
                DebugLog.Log($"[{Prefix}] > Pawn: \"{pawnName}\"; Weapon: \"{pawn.equipment?.Primary?.ToStringSafe()}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
                DebugLog.Log($"[{Prefix}] Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");
#if DEBUG
                HarmonyLog.DumpStackTrace();
#endif
                // TODO: fix the case with weapons being dropped due to arm/torse (?) desctruction.
                if (GlobalState.CanSkipNextCallOfDropAllEquipment)
                {
                    GlobalState.CanSkipNextCallOfDropAllEquipment = false;

                    bool isColonist = pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer; // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?

                    if (isColonist && GlobalState.ModSettings.KeepColonistsWeapons && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfDeadColonists))
                    {
                        DebugLog.Log($"[{Prefix}] [-] Preventing original method execution! Colonis (\"{pawnName}\") will keep its weapon.");
                        return false;
                    }
                    else if (!isColonist && GlobalState.ModSettings.KeepOtherPawnsWeapons && (!pawn.Dead || GlobalState.ModSettings.KeepWeaponsAndInventoryOfOtherDeadPawns))
                    {
                        DebugLog.Log($"[{Prefix}] [-] Preventing original method execution! Non-player's pawn (\"{pawnName}\") will keep its weapon.");
                        return false;
                    }
                    else
                        DebugLog.Log($"[{Prefix}] [+] Original method will be executed. Pawn (\"{pawnName}\") will drop its weapon.");
                }
                else
                    DebugLog.Log($"[{Prefix}] [X] Original undisturbed method will be executed. Pawn: \"{pawnName}\".");
            }
            catch (Exception ex)
            {
                DebugLog.Log($"[{Prefix}] Exception: {ex}.");
            }

            return true;
        }
    }
}
