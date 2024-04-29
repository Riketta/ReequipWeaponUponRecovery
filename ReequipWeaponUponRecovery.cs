using HarmonyLib;
using HugsLib;
using HugsLib.Utils;
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
    public class ReequipWeaponUponRecovery : ModBase
    {
        public static readonly string PackageID = $"Riketta_{nameof(ReequipWeaponUponRecovery)}";

        protected override bool HarmonyAutoPatch => true;
        public override string LogIdentifier => nameof(ReequipWeaponUponRecovery);
        public override string SettingsIdentifier => nameof(ReequipWeaponUponRecovery);

        static ReequipWeaponUponRecovery() { }

        ReequipWeaponUponRecovery() : base()
        {
        }

        public override void EarlyInitialize()
        {
        }

        public override void StaticInitialize()
        {
            Logger.Message($"ReequipWeaponUponRecovery.PackageID: {ReequipWeaponUponRecovery.PackageID}.");
            Logger.Message($"HugsLib.ModContentPack.PackageId: {ModContentPack.PackageId}.");
            Logger.Message($"[{HarmonyInst.Id}] {nameof(ReequipWeaponUponRecovery)} patches applied.");
            HarmonyLog.Log($"[{HarmonyInst.Id}] {nameof(ReequipWeaponUponRecovery)} patches applied.");
        }

        public override void SettingsChanged()
        {
            base.SettingsChanged();
        }
    }
}
