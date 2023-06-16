using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blast Data")]
public class BlastWaveDataSO : ScriptableObject
{
    public int pointsCount =25;
    public int damage =30;
    public float maxRadius =20;
    public float speed =1f;
    public float startWidth =20;
    public float force =10;
}
