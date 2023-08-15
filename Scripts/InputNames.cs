﻿using Godot;

/// <summary>
/// Storing static references to input string names
/// </summary>
public static class InputNames
{
    public static StringName Up { get; } = new StringName("Up");
    public static StringName Left { get; } = new StringName("Left");
    public static StringName Down { get; } = new StringName("Down");
    public static StringName Right { get; } = new StringName("Right"); 
    public static StringName Jump { get; } = new StringName("Jump");
    public static StringName Fire { get; } = new StringName("Fire");
}