using System;
using Godot;

[System.Serializable]
public partial class Beat : Resource
{
    [Export]
    public float BeatTime { get; set; } // How much time elapsed in number of beats
    [Export]
    public int Note { get; set; }
    
    public String HitRating { get; set; }
}