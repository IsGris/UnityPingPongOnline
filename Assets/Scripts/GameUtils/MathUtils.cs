using UnityEngine;

public static class MathUtils
{
    public static Vector3 Vector3(this Vector2 vec2) => new Vector3(vec2.x, vec2.y, 0);
}
