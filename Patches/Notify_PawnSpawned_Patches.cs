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

            DebugLog.Log($"[{Prefix}] Pawn ({pawn.Name}); Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}. Skipping function call!");
            DebugLog.Log($"[{Prefix}] Caller: {DebugLog.GetCallingClassAndMethodNames()}.");

            // Bypass "if (HasAnything() && pawn.Downed && !pawn.GetPosture().InBed()) { <...> DropAllEquipment(pawn.Position); }" if pawn player controlled.
            if (pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer) // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?
            {
                // TODO: drop loot on pawn death.
                DebugLog.Log($"[{Prefix}] Skipping method call!");
                return false;
            }

            return true;
        }
    }
}
