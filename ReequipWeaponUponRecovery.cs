using HarmonyLib;
using HugsLib;
using HugsLib.Settings;
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
        public static readonly string PackageID = $"Riketta.{nameof(ReequipWeaponUponRecovery)}";

        protected override bool HarmonyAutoPatch => true;
        public override string LogIdentifier => new string(nameof(ReequipWeaponUponRecovery).Where(c => char.IsUpper(c)).ToArray());
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
            GlobalState.Logger = Logger;

            Logger.Trace($"{nameof(ReequipWeaponUponRecovery)} patches applied.");
            HarmonyLog.Log($"[{PackageID}] {nameof(ReequipWeaponUponRecovery)} patches applied.");
        }

        public override void DefsLoaded()
        {
            GlobalState.ModSettings = new ModSettings(Settings);
        }

        public override void SettingsChanged()
        {
            Logger.Trace("Settings updated:");
            Logger.Trace($"> {nameof(GlobalState.ModSettings.KeepColonistsWeapons)} = {GlobalState.ModSettings.KeepColonistsWeapons}.");
            Logger.Trace($"> {nameof(GlobalState.ModSettings.KeepColonistsInventory)} = {GlobalState.ModSettings.KeepColonistsInventory}.");
            Logger.Trace($"> {nameof(GlobalState.ModSettings.KeepWeaponsAndInventoryOfDeadColonists)} = {GlobalState.ModSettings.KeepWeaponsAndInventoryOfDeadColonists}.");
            Logger.Trace($"> {nameof(GlobalState.ModSettings.KeepOtherPawnsWeapons)} = {GlobalState.ModSettings.KeepOtherPawnsWeapons}.");
            Logger.Trace($"> {nameof(GlobalState.ModSettings.KeepOtherPawnsInventory)} = {GlobalState.ModSettings.KeepOtherPawnsInventory}.");
            Logger.Trace($"> {nameof(GlobalState.ModSettings.KeepWeaponsAndInventoryOfOtherDeadPawns)} = {GlobalState.ModSettings.KeepWeaponsAndInventoryOfOtherDeadPawns}.");
            Logger.Trace($"> {nameof(GlobalState.ModSettings.Debug)} = {GlobalState.ModSettings.Debug}.");
            Logger.Trace($"> {nameof(GlobalState.Debug)} = {GlobalState.Debug}.");
            Logger.Trace($"> {nameof(Prefs.DevMode)} = {Prefs.DevMode}.");
        }
    }
}
