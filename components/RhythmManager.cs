using Godot;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


public partial class RhythmManager : Node
{
    [Export]
    public RhythmData RhythmData { get; set; }


    private RhythmUI rhythmUI;

    public Dictionary<String, double> hitRatingBeatOffset = new Dictionary<string, double>
    {
        { "perfect", 0.1 },
        { "good", 0.2 },
        { "hit", 0.4 }
    };
    double currentTime;
    public double BeatTime
    {
        get => RhythmData.ConvertTimeToBeatTime(currentTime);
    }
    private double beatSpawnOffsetBeatTime;

    public void StartTrack()
    {
        SetProcess(true);
    }

    public void StopTrack()
    {
        SetProcess(false);
    }

    public override void _Ready()
    {
        SetProcess(false);
        
        rhythmUI = GetNode<RhythmUI>("../RhythmUI");

        Callable.From(() => 
        {
            beatSpawnOffsetBeatTime = (rhythmUI.containerLength - 2*rhythmUI.beatUIWidth - (hitRatingBeatOffset["hit"] * rhythmUI.beatTravelLength)) / rhythmUI.beatTravelLength;
        }).CallDeferred();
        
    }

    public override void _Process(double delta)
    {
        currentTime += delta;

        // Mark all beats far behind as miss
        foreach (Beat beat in RhythmData.Beats)
        {
            if (beat != null && beat?.beatUI == null && (beat?.BeatTime - BeatTime - beatSpawnOffsetBeatTime) < 0.1)
            {
                rhythmUI.SpawnBeat(beat);
            }

            if (beat.HitRating != null) continue;
            double beatTimeDiff = BeatTime - beat.BeatTime;
            if (beatTimeDiff > hitRatingBeatOffset["hit"])
            {
                beat.HitRating = "miss";
            }
        }
    }

    // Get the hit rating of a beat given time, and set the beat's hit rating
    public String SetHitRatingForCurrentTime()
    {
        Beat closestBeat = RhythmData.GetClosestAvailableBeat(BeatTime);
        if (closestBeat == null) return "miss";
        double beatTimeDiff = Math.Abs(closestBeat.BeatTime - BeatTime);
        if (beatTimeDiff > hitRatingBeatOffset["hit"]) return "miss";
        foreach (KeyValuePair<String, double> entry in hitRatingBeatOffset)
        {
            if (beatTimeDiff < entry.Value)
            {
                closestBeat.HitRating = entry.Key;
                return entry.Key;
            }
        }
        return "miss"; // Return miss if no beat is within any rating
    }
}
