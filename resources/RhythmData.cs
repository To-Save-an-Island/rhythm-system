using Godot;
using System;

public partial class RhythmData : Resource
{
    [Export]
    public int BPM { get; set; }
    [Export]
    public Beat[] Beats { get; set; }

    // Get closest beat that has no hit rating
    public Beat GetClosestAvailableBeat(double beatTime)
    {
        double minDiff = double.MaxValue;
        Beat validBeat = null;

        foreach (Beat beat in Beats)
        {
            double diff = Math.Abs(beat.BeatTime - beatTime);
            if (beat.HitRating == null && diff < minDiff)
            {
                minDiff = diff;
                validBeat = beat;
            }
        }
        return validBeat;
    }

    public Beat GetNextBeat(double beatTime)
    {
        foreach (Beat beat in Beats)
        {
            if (beat.BeatTime > beatTime) return beat;
        }
        return null;
    }

    public double ConvertTimeToBeatTime(double time)
    {
        return time * BPM / 60;
    }
    public double ConvertBeatTimeToTime(double beatTime)
    {
        return beatTime * 60 / BPM;
    }
}
