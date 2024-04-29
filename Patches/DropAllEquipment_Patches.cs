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
    /// public void DropAllEquipment(IntVec3 pos, bool forbid = true, bool rememberPrimary = false)
    /// </summary>
    [HarmonyPatch(typeof(Pawn_EquipmentTracker), "DropAllEquipment")]
    internal class DropAllEquipment_Patches
    {
        private const string Prefix = "Pawn_EquipmentTracker.DropAllEquipment";

        [HarmonyPrefix]
        public static bool DropAllEquipment(Pawn_EquipmentTracker __instance, bool forbid, bool rememberPrimary)
        {
            var pawn = __instance.pawn;
            if (pawn is null)
                return true;

            DebugLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Player controlled: {pawn.IsPlayerControlled}; Faction: {pawn.Faction?.Name}; Dead: {pawn.Dead}.");
            DebugLog.Log($"[{Prefix}] Forbid: {forbid}; Remember Primary: {rememberPrimary}.");
            DebugLog.Log($"[{Prefix}] Caller: {DebugLog.GetCallingClassAndMethodNames()}.");
#if DEBUG
            DebugLog.DumpStackTrace();
#endif

            // TODO: get rid of "pawn.IsPlayerControlled" and keep just faction?
            if ((GlobalState.KeepOthersPawnWeapon || (GlobalState.KeepPlayersPawnWeapon && pawn.IsPlayerControlled && pawn.Faction == Faction.OfPlayer)) &&
                (!pawn.Dead || GlobalState.KeepWeaponAndInventoryForDeadPawn))
            {
                // Pretty slow execution (reflections) but in this case it is should be okay due to rare checks - only when unit downed/stripped.
                var callerMethod = new StackTrace().GetFrame(2).GetMethod();
                string classAndMethodTogether = $"{callerMethod.ReflectedType.Name}.{callerMethod.Name}";

                // Insanely inefficient check, but since original caller can be patched - it is required.
                // For example it can be either "Verse.Pawn.DropAndForbidEverything" or "MonoMod.Utils.DynamicMethodDefinition.Verse.Pawn.DropAndForbidEverything_Patch1".
                if (classAndMethodTogether.Contains("Pawn.DropAndForbidEverything") || classAndMethodTogether.Contains("Pawn_EquipmentTracker.Notify_PawnSpawned"))
                {
                    DebugLog.Log($"[{Prefix}] Caller: \"{classAndMethodTogether}\" - preventing original method execution!");
                    return false;
                }
            }

            return true;
        }
    }
}
