using UnityEngine;

public static class Helper
{
    public static Vector2 GetRandom(Vector2 min, Vector2 max)
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }
}

