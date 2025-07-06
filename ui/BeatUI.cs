using Godot;
using System;

public partial class BeatUI : ColorRect
{
    [Export]
    public PackedScene RatingLabelScene { get; set; }
    public string hitRatingText;
    public double speed;

    public void FadeOut()
    {
        SetProcess(false);

        RatingLabel ratingLabel = RatingLabelScene.Instantiate<RatingLabel>();
        GetParent().AddChild(ratingLabel);
        ratingLabel.Position = new Vector2(Position.X, Position.Y - Size.Y); // Set Position.Y
        ratingLabel.SetRating(hitRatingText);

        Tween fadeTween = GetTree().CreateTween();
        fadeTween.TweenProperty(this, "modulate", new Color(1, 1, 1, 0), 0.2);
        fadeTween.Finished += QueueFree;
    }

    public override void _Process(double delta)
    {
        Position = new Vector2((float)(Position.X - speed * delta), Position.Y);
    }
}
