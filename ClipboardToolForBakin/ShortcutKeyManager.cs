using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static ClipboardToolForBakin2.FormKeysSetting;

namespace ClipboardToolForBakin2
{
    public static class ShortcutKeyManager
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shortcutkeys.json");
        public static readonly string _Nothing = "Nothing";

        public static void InitKeyBindings(List<FormKeysSetting.ShortcutKeyBinding> shortcutKeyBindings)
        {
            shortcutKeyBindings.Clear();
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Duplicate row", ShortcutKey = "Ctrl+Shift+D" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Preview", ShortcutKey = "Ctrl+Shift+P" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Reload", ShortcutKey = "Ctrl+Shift+R" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Skip Streaming", ShortcutKey = "Ctrl+Shift+F" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Next row", ShortcutKey = "Ctrl+Right" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Previous row", ShortcutKey = "Ctrl+Left" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Move up", ShortcutKey = "Ctrl+Shift+Up" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Move down", ShortcutKey = "Ctrl+Shift+Down" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Apply", ShortcutKey = "Ctrl+Return" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Tag", ShortcutKey = "Ctrl+H" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "NPL", ShortcutKey = "Ctrl+J" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "NPC", ShortcutKey = "Ctrl+K" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "NPR", ShortcutKey = "Ctrl+L" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Text", ShortcutKey = "Ctrl+T" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Talk cast1", ShortcutKey = "Ctrl+Shift+Left" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Talk cast2", ShortcutKey = "Ctrl+Shift+Right" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Cast1", ShortcutKey = "Ctrl+B" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Cast2", ShortcutKey = "Ctrl+N" });

            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Input Helper", ShortcutKey = "Ctrl+M" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Save", ShortcutKey = "Ctrl+S" });
            shortcutKeyBindings.Add(new ShortcutKeyBinding { FunctionName = "Save As...", ShortcutKey = "Ctrl+Shift+S" });
        }

        public static void LoadShortcutKeys(ref List<FormKeysSetting.ShortcutKeyBinding> shortcutKeyBindings)
        {
            if (File.Exists(filePath))
            {
                var jsonString = File.ReadAllText(filePath);
                var result = JsonSerializer.Deserialize<List<FormKeysSetting.ShortcutKeyBinding>>(jsonString);
                if (result != null)
                {
                    shortcutKeyBindings = result;
                }
                else
                {
                    InitKeyBindings(shortcutKeyBindings);
                }
            }
            else
            {
                InitKeyBindings(shortcutKeyBindings);
            }
        }

        public static void SaveShortcutKeys(List<FormKeysSetting.ShortcutKeyBinding> shortcutKeyBindings)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(shortcutKeyBindings, options);
            File.WriteAllText(filePath, jsonString);
        }

        public static string KeyCodeToString(KeyEventArgs e)
        {
            List<string> keys = new List<string>();
            if (e.Control) keys.Add("Ctrl");
            if (e.Alt) keys.Add("Alt");
            if (e.Shift) keys.Add("Shift");
            if (e.KeyCode != Keys.None
                && e.KeyCode != Keys.ShiftKey
                && e.KeyCode != Keys.ControlKey
                && e.KeyCode != Keys.Menu)
            {
                keys.Add(e.KeyCode.ToString());
            }
            return string.Join("+", keys);
        }
    }
}
