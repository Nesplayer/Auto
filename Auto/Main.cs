using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace Auto
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public static Main Instance{ get; private set; }
        
        public override void Load()
        {
            Logger("Auto is loading..", "Auto");
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        public static void Logger(String msg, String source, LogLevel level = LogLevel.Info)
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
