using Godot;
using System;

public partial class RhythmUI : Panel
{
    [Export]
    public RhythmData RhythmData { get; set; }
    [Export]
    public PackedScene BeatUIScene { get; set; }
    [Export]
    public RhythmManager RhythmManager { get; set; }
    [Export]
    public double beatUIWidth { get; set; } = 4.0; // Pixels

    public double containerLength;
    public double beatTravelLength = 41.5; // FIXME: magic number
    private Control container;
    private ColorRect beatHitMarker;
    private Node2D beatSpawnPosition;

    public override void _Ready()
    {
        container = GetNode<Control>("MarginContainer/Control");
        container.Resized += () =>
        {
            containerLength = GetNode<Control>("MarginContainer/Control").GetRect().Size.X;
            beatSpawnPosition.Position = new Vector2((float)(containerLength - beatUIWidth), 0);
        };

        beatHitMarker = GetNode<ColorRect>("MarginContainer/Control/BeatHitMarker");
        beatHitMarker.Size = new Vector2((float)beatUIWidth, beatHitMarker.Size.Y);
        beatHitMarker.Position = new Vector2((float)(RhythmManager.hitRatingBeatOffset["hit"] * beatTravelLength), 0);

        beatSpawnPosition = GetNode<Node2D>("MarginContainer/Control/BeatSpawnPosition");
    }

    public void SpawnBeat(Beat beat)
    {
        BeatUI beatUIInstance = BeatUIScene.Instantiate<BeatUI>();
        beat.beatUI = beatUIInstance;

        beatSpawnPosition.AddChild(beatUIInstance);
        beatUIInstance.Size = new Vector2((float)beatUIWidth, beatUIInstance.Size.Y);
        beatUIInstance.speed = beatTravelLength / RhythmData.ConvertBeatTimeToTime(1); 
    }
}
