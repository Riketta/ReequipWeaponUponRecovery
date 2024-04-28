using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ReequipWeaponUponRecovery
{
    [StaticConstructorOnStartup]
    public class ReequipWeaponUponRecovery
    {
        public static readonly string HarmonyID = $"Riketta_{nameof(ReequipWeaponUponRecovery)}";

        static ReequipWeaponUponRecovery()
        {
            var harmony = new Harmony(HarmonyID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            DebugLog.Log($"[{HarmonyID}] {nameof(ReequipWeaponUponRecovery)} patches applied.");
        }
    }
}
