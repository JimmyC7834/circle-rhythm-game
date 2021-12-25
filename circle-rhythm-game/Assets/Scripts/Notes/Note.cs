using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum JudgementGrade { Perfect, Great, Good, Miss, Count }
    public float idealTime;
    protected float radius = 6f;
    protected float angle;
    protected float startTime;
    protected float speed;
    protected float angleSpeed;

    public float Radius(float timeLapsed) => Mathf.Max(0, radius - speed * timeLapsed);
    public float Theta(float timeLapsed) => angle - angleSpeed * timeLapsed;
    public Vector2 Position(float timeLapsed) => new Vector2(
        Mathf.Cos(Theta(timeLapsed) * Mathf.Deg2Rad) * Radius(timeLapsed), 
        Mathf.Sin(Theta(timeLapsed) * Mathf.Deg2Rad) * Radius(timeLapsed)
    );

    public void Initialize(ChartSO.SpawnInfo info) => Initialize(info.idealTime, info.angle, info.speed, info.angleSpeed);
    public void Initialize(float idealTime, float angle, float speed, float angleSpeed)
    {
        this.idealTime = idealTime;
        this.angle = angle;
        this.speed = speed;
        this.angleSpeed = angleSpeed;
    }

    protected virtual float[] JudgementRange => new[] { .1f, .25f, .35f, .5f };
    
    public JudgementGrade GetGradeAtTime(float time)
    {
        // clamps the offset between late miss and fast good
        // float offset = Mathf.Clamp(idealTime - time, -JudgementRange[(int)JudgementGrade.Count - 1],JudgementRange[(int)JudgementGrade.Count - 2]);
        float offset = idealTime - time;
        for(int i = 0; i < (int) JudgementGrade.Count; i++)
        {
            if (Mathf.Abs(offset) < JudgementRange[i])
                return (JudgementGrade) i;
        }

        return JudgementGrade.Miss;
    }
}