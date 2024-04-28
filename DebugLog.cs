using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReequipWeaponUponRecovery
{
    internal class DebugLog
    {
        public static void Log(string message)
        {
#if DEBUG
            FileLog.Log($"[{DateTime.Now:s}] {message}");
#endif
        }

        public static string GetCallingClassAndMethodNames()
        {
            var method = new StackTrace().GetFrame(3).GetMethod();
            var className = method.ReflectedType.Name;

            return $"{className}.{method.Name}()";
        }
    }
}
