using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Chart")]
public class ChartSO : ScriptableObject
{
    [Serializable]
    public struct SpawnInfo
    {
        public Note.NoteType noteType;
        public float idealTime;
        public float angle;
        public float speed;
        public float deltaAngle;
        public float duration;
        public float SpawnTime(float radius) => idealTime - radius / speed;
    }
    public float bpm;
    public float radius;
    public SpawnInfo[] spawnInfos;
}
