using Godot;
using System;

public partial class RhythmComponent : Node2D
{
    [Export]
    public RhythmData RhythmData { get; set; }

    private RhythmManager rhythmManager;
    private RhythmUI rhythmUI;

    private Boolean started = false;

    public override void _Ready()
    {
        rhythmManager = GetNode<RhythmManager>("RhythmManager");
        rhythmManager.RhythmData = RhythmData;

        rhythmUI = GetNode<RhythmUI>("RhythmUI");
        rhythmUI.RhythmData = RhythmData;
    }

    public override void _Input(InputEvent @event)
    {
        if (started) return;

        if (Input.IsActionJustPressed("instrument"))
        {
            started = true;
            rhythmManager.StartTrack();
        }
    }
}
