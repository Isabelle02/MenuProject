using UnityEngine.UI;

public static class TextExtensions
{
    public static void SetAlpha(this Text text, float alpha)
    {
        var color = text.color;
        color.a = alpha;
        text.color = color;
    }
}