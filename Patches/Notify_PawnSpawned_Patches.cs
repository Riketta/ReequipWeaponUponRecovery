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
    /// public void Notify_PawnSpawned().
    /// Caller of Pawn_EquipmentTracker.DropAllEquipment.
    /// </summary>
    [HarmonyPatch(typeof(Pawn_EquipmentTracker), "Notify_PawnSpawned")]
    internal class Notify_PawnSpawned_Patches
    {
        private const string Prefix = "Pawn_EquipmentTracker.Notify_PawnSpawned";

        [HarmonyPrefix]
        public static bool Notify_PawnSpawnedPrefix(Pawn_EquipmentTracker __instance)
        {
            Pawn pawn = __instance.pawn;
            if (pawn is null)
                return true;

            HarmonyLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
            HarmonyLog.Log($"[{Prefix}] Caller: {HarmonyLog.GetCallingClassAndMethodNames()}.");

            GlobalState.CanSkipNextCallOfDropAllEquipment = true;

            return true;
        }

        [HarmonyPostfix]
        public static void Notify_PawnSpawnedPostfix()
        {
            GlobalState.CanSkipNextCallOfDropAllEquipment = false;
        }
    }
}
