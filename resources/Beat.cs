using System;
using Godot;

[System.Serializable]
public partial class Beat : Resource
{
    [Export]
    public double BeatTime { get; set; } // How much time elapsed in number of beats
    [Export]
    public int Note { get; set; }

    private String hitRating;
    public String HitRating
    {
        get => hitRating;
        set
        {
            hitRating = value;

            if (beatUI == null) return;
            beatUI.hitRatingText = value;
            beatUI.FadeOut();
        }
    }

    public BeatUI beatUI;
    public double spawnTime;
    public bool isSpawned = false;
}