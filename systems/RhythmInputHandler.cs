using Godot;
using System;

public partial class RhythmInputHandler : Node
{

    [Export]
    RhythmManager rhythmManager;

    public override void _Ready()
    {
        rhythmManager.StartTrack();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("instrument"))
        {
            GD.Print(rhythmManager.SetHitRatingForCurrentTime());
        }
    }

}
