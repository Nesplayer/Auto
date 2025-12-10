using System.Reflection;
using Auto;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

[assembly: AssemblyFileVersion(Main.PluginVersion)]
[assembly: AssemblyInformationalVersion(Main.PluginVersion)]
[assembly: AssemblyVersion(Main.PluginVersion)]

namespace Auto
{
    [BepInPlugin(PluginGuid, "Auto-Reworked", PluginVersion)]
    [BepInProcess("Among Us.exe")]
    public class Main : BasePlugin
    {
        // 模组基本设定
        public const string ModName = "Auto-Reworked"; // 模组名字
        public const string ForkId = "Auto-Reworked"; // 模组 fork id
        public const string PluginVersion = "1.0.0"; // 版本号
        public const string PluginGuid = "cn.nes.Auto-Reworked"; // 模组 GUID

        // 版本相关设定
        public const string LowestSupportedVersion = "2025.11.18"; // 17.1.0

        public override void Load()
        {
            Logger("Auto is loading..", "Auto");
        }

        public static void Logger(string msg, string source, LogLevel level = LogLevel.Info)
        {
            ManualLogSource log = BepInEx.Logging.Logger.CreateLogSource(source);
            switch(level)
            {
                case LogLevel.Debug:
                    log.LogDebug(msg);
                    break;
                case LogLevel.Info:
                    log.LogInfo(msg);
                    break;
                case LogLevel.Warning:
                    log.LogWarning(msg);
                    break;
                case LogLevel.Error:
                    log.LogError(msg);
                    break;
                case LogLevel.Fatal:
                    log.LogFatal(msg);
                    break;
                default:
                    break;
            }
        }
    }
}
