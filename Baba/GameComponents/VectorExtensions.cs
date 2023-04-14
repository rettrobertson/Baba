using Microsoft.Xna.Framework;

public static class VectorExtensions
{
    public static Vector2 Normalized(this Vector2 vector)
    {
        vector.Normalize();
        return vector;
    }
}
