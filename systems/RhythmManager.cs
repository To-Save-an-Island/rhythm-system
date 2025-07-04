using Godot;
using System;
using System.Collections.Generic;


public partial class RhythmManager : Node
{
    [Export]
    public RhythmData RhythmData { get; set; }

    public Dictionary<String, double> hitRatingBeatOffset = new Dictionary<string, double>
    {
        { "perfect", 0.2 },
        { "good", 0.6 },
        { "hit", 1.0 }
    };

    double currentTime;
    public double BeatTime
    {
        get => RhythmData.ConvertTimeToBeatTime(currentTime);
    }

    public void StartTrack()
    {
        SetProcess(true);
    }

    public void StopTrack()
    {
        SetProcess(false);
    }

    public override void _Process(double delta)
    {
        currentTime += delta;

        // TEMP: Test output in console
        foreach (Beat beat in RhythmData.Beats)
        {
            GD.Print(beat.BeatTime + " " + beat.HitRating + " ");
        }
        GD.Print("----------");
        

        // Mark all beats far behind as miss
        foreach (Beat beat in RhythmData.Beats)
        {
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
