using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ReequipWeaponUponRecovery.Patches
{
    /// <summary>
    /// private void MakeDowned(DamageInfo? dinfo, Hediff hediff).
    /// </summary>
    [HarmonyPatch(typeof(Pawn_HealthTracker), "MakeDowned")]
    internal class MakeDowned_Patch
    {
        private const string Prefix = "Pawn_HealthTracker.MakeDowned";

#if DEBUG
        [HarmonyPrefix]
        public static bool LogMakeDowned(Pawn_HealthTracker __instance, Pawn ___pawn)
        {
            var pawn = ___pawn;
            if (pawn is null)
                return true;

            DebugLog.Log($"[{Prefix}] Pawn: {pawn.Name}; Primary: {pawn.equipment?.Primary?.ToStringSafe()}; PrimaryEq: {pawn.equipment?.PrimaryEq?.ToStringSafe()}; Spawned: {pawn.Spawned}.");

            return true;
        }
#endif
    }
}
