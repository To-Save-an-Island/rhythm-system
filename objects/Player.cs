using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector("left", "right", "up", "down");
        Velocity = direction * 200;
        MoveAndSlide();
    }
}
