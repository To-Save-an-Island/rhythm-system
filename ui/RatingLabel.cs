using Godot;
using System;

public partial class RatingLabel : Label
{
    private Vector2 driftVector = Vector2.Up;
    private float driftSpeed = 10.0f; // Pixels per second

    public override void _Ready()
    {
        SetProcess(false);
    }

    public override void _Process(double delta)
    {
        Position += driftVector * driftSpeed * (float)delta;
    }

    public void SetRating(string rating)
    {
        GD.Print(rating);

        Text = rating;

        switch (rating)
        {
            case "perfect":
                Modulate = Colors.Green;
                break;
            case "good":
                Modulate = Colors.Yellow;
                break;
            case "hit":
                Modulate = Colors.Orange;
                break;
            default:
                Modulate = Colors.Red;
                break;
        }

        Position = new Vector2(Position.X - GetRect().Size.X / 2, Position.Y);

        SetProcess(true);

        Tween fadeTween = GetTree().CreateTween();
        fadeTween.TweenProperty(this, "modulate:a", 0, 0.4);
        fadeTween.Finished += QueueFree;
    }
}
