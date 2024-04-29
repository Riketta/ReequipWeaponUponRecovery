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
    /// </summary>
    [HarmonyPatch(typeof(Pawn_EquipmentTracker), "Notify_PawnSpawned")]
    internal class Notify_PawnSpawned_Patches
    {
        private const string Prefix = "Pawn_EquipmentTracker.Notify_PawnSpawned";

        [HarmonyPrefix]
        public static bool Notify_PawnSpawned(Pawn_EquipmentTracker __instance)
        {
            Pawn pawn = __instance.pawn;
            if (pawn is null)
                return true;

            DebugLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
            DebugLog.Log($"[{Prefix}] Caller: {DebugLog.GetCallingClassAndMethodNames()}.");

            return true;
        }
    }
}
