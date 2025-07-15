using Godot;
using System;

public partial class RhythmUI : PanelContainer
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
	public double beatTravelLength;
	private Control container;
	private ColorRect beatHitMarker;
	private Node2D beatSpawnPosition;

	public override void _Ready()
	{
		container = GetNode<Control>("MarginContainer/Control");
		beatSpawnPosition = container.GetNode<Node2D>("BeatSpawnPosition");

		container.Resized += () =>
		{
			containerLength = container.GetRect().Size.X;
			beatSpawnPosition.Position = new Vector2((float)(containerLength - beatUIWidth), 0);
			beatTravelLength = (containerLength - beatUIWidth) / 4; // FIXME: magic number (number of beats in a panel UI)
		};

		Callable.From(() =>
		{
			foreach (Beat beat in RhythmData.Beats)
			{
				if (beat.spawnTime != 0) continue;
				beat.spawnTime = beat.BeatTime - ((containerLength - 2 * beatUIWidth - (RhythmManager.hitRatingBeatOffset["hit"] * beatTravelLength)) / beatTravelLength);
			}
			beatHitMarker = container.GetNode<ColorRect>("BeatHitMarker");
			beatHitMarker.Size = new Vector2((float)beatUIWidth, beatHitMarker.Size.Y);
			beatHitMarker.Position = new Vector2((float)(RhythmManager.hitRatingBeatOffset["hit"] * beatTravelLength), 0);
		}).CallDeferred();
	}

	public void SpawnBeat(Beat beat)
	{
		GD.Print("SpawnBeat: " + beat.BeatTime);
		BeatUI beatUIInstance = BeatUIScene.Instantiate<BeatUI>();
		beat.beatUI = beatUIInstance;
		beat.isSpawned = true;

		beatSpawnPosition.AddChild(beatUIInstance);
		beatUIInstance.Size = new Vector2((float)beatUIWidth, beatUIInstance.Size.Y);
		beatUIInstance.speed = beatTravelLength / RhythmData.ConvertBeatTimeToTime(1); 
	}
}
