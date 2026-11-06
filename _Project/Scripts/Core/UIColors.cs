using UnityEngine;
using UnityEngine.UIElements;

public static class UIColors
{
    // Фоны
    public static readonly Color BG_Main = Hex("#0D0D0D");
    public static readonly Color BG_Card = Hex("#1A1A1A");
    public static readonly Color BG_Card2 = Hex("#252525");
    public static readonly Color BG_Input = Hex("#1E1E1E");

    // Красный акцент
    public static readonly Color Red = Hex("#E8282B");
    public static readonly Color RedDark = Hex("#B01E21");
    public static readonly Color RedGlow = Hex("#FF3B3F");

    // Текст
    public static readonly Color TextWhite = Hex("#FFFFFF");
    public static readonly Color TextGray = Hex("#9A9A9A");
    public static readonly Color TextMuted = Hex("#555555");

    // Бордеры
    public static readonly Color Border = Hex("#2A2A2A");
    public static readonly Color BorderActive = Hex("#E8282B");

    // Специальные
    public static readonly Color UNESCO = Hex("#F5A623");
    public static readonly Color ARBadge = Hex("#E8282B");
    public static readonly Color Success = Hex("#34C759");
    public static readonly Color Error = Hex("#FF3B30");

    private static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color c);
        return c;
    }
}
