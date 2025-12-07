using AmongUs.Data;
using UnityEngine;

namespace Auto.Patches;

[HarmonyPatch(typeof(ChatController), nameof(ChatController.Update))]
class ChatControllerUpdatePatch
{
    [HarmonyPrefix]
    public static void Prefix(ChatController __instance)
    {
        if (AmongUsClient.Instance.AmHost && DataManager.Settings.Multiplayer.ChatMode == InnerNet.QuickChatModes.QuickChatOnly)
            DataManager.Settings.Multiplayer.ChatMode = InnerNet.QuickChatModes.FreeChatOrQuickChat;
    }

    [HarmonyPostfix]
    public static void Postfix(ChatController __instance)
    {
        var backgroundColor = new Color32(40, 40, 40, byte.MaxValue);

        // free chat
        __instance.freeChatField.background.color = backgroundColor;
        __instance.freeChatField.textArea.compoText.Color(Color.white);
        __instance.freeChatField.textArea.outputText.color = Color.white;

        // quick chat
        __instance.quickChatField.background.color = backgroundColor;
        __instance.quickChatField.text.color = Color.white;

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.C))
            ClipboardHelper.PutClipboardString(__instance.freeChatField.textArea.text);

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.V))
            __instance.freeChatField.textArea.SetText(__instance.freeChatField.textArea.text + GUIUtility.systemCopyBuffer);

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.X))
        {
            ClipboardHelper.PutClipboardString(__instance.freeChatField.textArea.text);
            __instance.freeChatField.textArea.SetText("");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && ChatCommandPatch.ChatHistory.Any())
        {
            ChatCommandPatch.CurrentHistorySelection = Mathf.Clamp(--ChatCommandPatch.CurrentHistorySelection, 0, ChatCommandPatch.ChatHistory.Count - 1);
            __instance.freeChatField.textArea.SetText(ChatCommandPatch.ChatHistory[ChatCommandPatch.CurrentHistorySelection]);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && ChatCommandPatch.ChatHistory.Any())
        {
            ChatCommandPatch.CurrentHistorySelection++;
            if (ChatCommandPatch.CurrentHistorySelection < ChatCommandPatch.ChatHistory.Count)
                __instance.freeChatField.textArea.SetText(ChatCommandPatch.ChatHistory[ChatCommandPatch.CurrentHistorySelection]);
            else __instance.freeChatField.textArea.SetText("");
        }
    }
}
