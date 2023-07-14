using UnityEngine;

public static class GameObjectExtensions
{
    // Находится ли GameObject на определенной маске
    public static bool IsInLayer(this GameObject go, LayerMask layer)
    {
        return layer == (layer | 1 << go.layer);
    }
}