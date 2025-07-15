using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RhythmComponent : Node2D
{
    [Export]
    public RhythmData RhythmData { get; set; }

    private RhythmManager rhythmManager;
    private List<RhythmUI> rhythmUIs;

    private Boolean started = false;

    public override void _Ready()
    {
        rhythmManager = GetNode<RhythmManager>("RhythmManager");
        rhythmManager.RhythmData = RhythmData;

        rhythmUIs = [..GetNode("PanelContainer/VBoxContainer").GetChildren().Cast<RhythmUI>()];
        foreach (RhythmUI rhythmUI in rhythmUIs)
        {
            rhythmUI.RhythmManager = rhythmManager;
            rhythmUI.RhythmData = RhythmData;
        }

        rhythmManager.RhythmUIs = rhythmUIs;
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
