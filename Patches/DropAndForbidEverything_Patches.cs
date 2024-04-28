using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static HarmonyLib.Code;

namespace ReequipWeaponUponRecovery.Patches
{
    /// <summary>
    /// public void DropAndForbidEverything(bool keepInventoryAndEquipmentIfInBed = false, bool rememberPrimary = false).
    /// </summary>
    [HarmonyPatch(typeof(Pawn), "DropAndForbidEverything")]
    internal class DropAndForbidEverything_Patch
    {
        private const string Prefix = "Pawn.DropAndForbidEverything";

#if DEBUG
        [HarmonyPrefix]
        public static bool LogDropAndForbidEverything(Pawn __instance, ref bool keepInventoryAndEquipmentIfInBed, ref bool rememberPrimary)
        {
            var pawn = __instance;
            if (pawn is null)
                return true;

            DebugLog.Log($"[{Prefix}] Pawn: \"{pawn.Name}\"; Primary: {pawn.equipment?.Primary?.ToStringSafe()}; PrimaryEq: {pawn.equipment?.PrimaryEq?.ToStringSafe()}; Spawned: {pawn.Spawned}; {keepInventoryAndEquipmentIfInBed}; {rememberPrimary}.");

            return true;
        }
#endif

        /// <summary>
        /// Patch "if (!keepInventoryAndEquipmentIfInBed || !this.InBed())" to skip this conditional block and keep pawn with its weapon and inventory.
        /// </summary>
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> SkipBedValidation(IEnumerable<CodeInstruction> instructions)
        {
            // TODO: validate that pawn player controlled and belongs to player's faction. Hook and validate DropAllEquipment & DropAllNearPawn instead by checking caller.
            DebugLog.Log($"Applying SkipBedValidation Transpiler Patch.");

            var opcodes = new List<CodeInstruction>(instructions);

            DebugLog.Log($"===== Before Patch =====");
            for (var i = 0; i < opcodes.Count; i++)
                DebugLog.Log($"{opcodes[i]}");

            // Code to patch:
            // if (!keepInventoryAndEquipmentIfInBed || !this.InBed())
            // IL_00e7: ldarg.1
            // IL_00e8: brfalse.s IL_00f2
            // IL_00ea: ldarg.0
            // IL_00eb: call bool RimWorld.RestUtility::InBed(class Verse.Pawn)
            // IL_00f0: brtrue.s IL_0139
            // ...
            // IL_0139: ret

            for (var i = 0; i < opcodes.Count; i++)
                if (opcodes[i].opcode == OpCodes.Ldarg_1) // Look for "keepInventoryAndEquipmentIfInBed" access.
                {
                    opcodes[i].opcode = OpCodes.Ldc_I4_1; // Replace keepInventoryAndEquipmentIfInBed value with "true".

                    int brtruesIndex = i + 2; // + 2 to skip brfalse.s.
                    while (opcodes[brtruesIndex].opcode != OpCodes.Brtrue_S) // Look for brtrue.s while nopping everything on the way.
                    {
                        opcodes[brtruesIndex].opcode = OpCodes.Nop;
                        opcodes[brtruesIndex].operand = null;
                        brtruesIndex++;
                    }

                    opcodes[brtruesIndex - 1].opcode = OpCodes.Ldc_I4_1; // Replace last nop previous to brtrue.s with "true".

                    break;
                }

            // TODO: patch this code instead to keep only weapon in inventory while still dropping other stuff (like intended in vanilla).
            // Code to patch:
            // Pawn_EquipmentTracker pawn_EquipmentTracker = this.equipment;
            // IL_00F2: ldarg.0
            // IL_00F3: ldfld     class Verse.Pawn_EquipmentTracker Verse.Pawn::equipment
            // if (pawn_EquipmentTracker != null)
            // IL_00F8: dup
            // IL_00F9: brtrue.s IL_00FE
            // IL_00FB: pop
            // IL_00FC: br.s IL_010B

            //for (var i = 0; i < opcodes.Count; i++)
            //{
            //}

            DebugLog.Log($"===== After Patch =====");
            for (var i = 0; i < opcodes.Count; i++)
                DebugLog.Log($"{opcodes[i]}");

            return opcodes.AsEnumerable();
        }
    }
}
