namespace Auto.Patches;

[HarmonyPatch(typeof(ChatController), nameof(ChatController.SendChat))]
internal class ChatCommandPatch
{
    public static List<string> ChatHistory = [];
    public static int CurrentHistorySelection = -1;

    [HarmonyPrefix]
    public static bool Prefix(ChatController __instance)
    {
        if (__instance.quickChatField.visible == false && __instance.freeChatField.textArea.text == "") return false;
        //__instance.timeSinceLastMessage = 3f;
        var text = __instance.freeChatField.textArea.text;
        if (ChatHistory.Count == 0 || ChatHistory[^1] != text) ChatHistory.Add(text);
        CurrentHistorySelection = ChatHistory.Count;
        Main.Logger($"Prefix: text='{text}'", "ChatController");
        return true;
    }
}
