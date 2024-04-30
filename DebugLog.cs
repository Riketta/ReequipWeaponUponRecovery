using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReequipWeaponUponRecovery
{
    internal class DebugLog
    {
        public static void Log(string message)
        {
            HarmonyLog.Log(message);
            GlobalState.Logger?.Trace(message);
        }
    }
}
