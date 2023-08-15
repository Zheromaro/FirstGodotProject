using Godot;

public static class Extensions
{

    // Returns raw input (either:-1,1,0) of any vector 2
    public static Vector2 GetRaw(this Vector2 vector)
    {
        vector.X = vector.X switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => 0
        };
        vector.Y = vector.Y switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => 0
        };

        return vector;
    }

}