using Godot;
using System;

public partial class RhythmData : Resource
{
    [Export]
    public int BPM { get; set; } = 60;
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

    public double ConvertTimeToBeatTime(double time)
    {
        return time * 60 / BPM;
    }
}
