using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MatchScore : ScriptableObject
{
    public int Match;
    public float Win;
    public float Lose;
    public float winRate;
    public float currentAvgTime;
}
