using HugsLib.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ReequipWeaponUponRecovery
{
    internal class ModSettings
    {
        public SettingHandle<bool> Debug;

        /// <summary>
        /// Keep weapons of player controlled pawns (colonists) with them on downed.
        /// </summary>
        public SettingHandle<bool> KeepColonistsWeapons;

        /// <summary>
        /// Keep inventory items (non-weapons & equipment) of player controlled pawns (colonists) with them on downed.
        /// </summary>
        public SettingHandle<bool> KeepColonistsInventory;

        /// <summary>
        /// Keep weapons (if <see cref="KeepColonistsWeapons"/> set to true) and inventory (if <see cref="KeepColonistsInventory"/> set to true) of player controlled pawns (colonists) with them or drop everything on death.
        /// This option allows <see cref="KeepColonistsWeapons"/> and <see cref="KeepColonistsInventory"/> to be applied to dead pawns.
        /// </summary>
        public SettingHandle<bool> KeepWeaponsAndInventoryOfDeadColonists;

        /// <summary>
        /// Keep weapons of non-player's pawns with them on downed.
        /// </summary>
        public SettingHandle<bool> KeepOtherPawnsWeapons;

        /// <summary>
        /// Keep inventory items (non-weapons & equipment) of non-player's pawns with them on downed.
        /// </summary>
        public SettingHandle<bool> KeepOtherPawnsInventory;

        /// <summary>
        /// Keep weapons (if <see cref="KeepOtherPawnsWeapons"/> set to true) and inventory (if <see cref="KeepOtherPawnsInventory"/> set to true) of non-player's pawns with them or drop everything on death. OP against tibal raids.
        /// This option allows <see cref="KeepOtherPawnsWeapons"/> and <see cref="KeepOtherPawnsInventory"/> to be applied to dead pawns.
        /// </summary>
        public SettingHandle<bool> KeepWeaponsAndInventoryOfOtherDeadPawns;

        private readonly ModSettingsPack _settings = null;

        public ModSettings(ModSettingsPack settings)
        {
            _settings = settings;

            // TODO: add support of translations.

            KeepColonistsWeapons = settings.GetHandle(nameof(KeepColonistsWeapons),
                "Keep Colonists Weapons",
                "Keep weapons of player controlled pawns (colonists) with them on downed.",
                true);

            KeepColonistsInventory = settings.GetHandle(nameof(KeepColonistsInventory),
                "Keep Colonists Inventory",
                "Keep inventory items (non-weapons & equipment) of player controlled pawns (colonists) with them on downed.",
                true);

            KeepWeaponsAndInventoryOfDeadColonists = settings.GetHandle(nameof(KeepWeaponsAndInventoryOfDeadColonists),
                "Keep Weapons And Inventory Of Dead Colonists",
                "Keep weapons and inventory (if according options toggled on) of player controlled pawns (colonists) with them or drop everything on death.",
                true);

            KeepOtherPawnsWeapons = settings.GetHandle(nameof(KeepOtherPawnsWeapons),
                "Keep Other Pawns Weapons",
                "Keep weapons of non-player's pawns with them on downed.",
                true);

            KeepOtherPawnsInventory = settings.GetHandle(nameof(KeepOtherPawnsInventory),
                "Keep Other Pawns Inventory",
                "Keep inventory items (non-weapons & equipment) of non-player's pawns with them on downed.",
                true);

            KeepWeaponsAndInventoryOfOtherDeadPawns = settings.GetHandle(nameof(KeepWeaponsAndInventoryOfOtherDeadPawns),
                "Keep Weapons And Inventory Of Other Dead Pawns",
                "Keep weapons and inventory (if according options toggled on) of non-player's pawns with them or drop everything on death. OP against tibal raids.",
                true);

            Debug = settings.GetHandle(nameof(Debug), "Debug", string.Empty, false);
        }
    }
}
