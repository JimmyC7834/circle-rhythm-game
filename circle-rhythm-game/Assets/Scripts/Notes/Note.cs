using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum JudgementGrade { Perfect, Great, Good, Miss, Count }
    public enum NoteType { Dot, Long, Circle }
    [SerializeField] protected float idealTime;
    [SerializeField] protected float radius = 6f;
    [SerializeField] protected float angle;
    [SerializeField] protected float startTime;
    [SerializeField] protected float speed;
    [SerializeField] protected float deltaAngle;

    protected float Radius(float timeLapsed) => Mathf.Max(0, radius - speed * timeLapsed);
    protected float Theta(float timeLapsed) =>  Mathf.Lerp(angle, angle + deltaAngle, Radius(timeLapsed)/radius);
    protected Vector2 Position(float timeLapsed) => new Vector2(
        Mathf.Cos(Theta(timeLapsed) * Mathf.Deg2Rad) * Radius(timeLapsed), 
        Mathf.Sin(Theta(timeLapsed) * Mathf.Deg2Rad) * Radius(timeLapsed)
    );

    public virtual float spawnTime => idealTime - radius / speed;
    protected virtual float[] judgementRange => new[] { .1f, .25f, .35f};
    [HideInInspector] public NoteType nodeType;

    protected virtual void OnEnable()
    {
        startTime = Time.time;
    }

    public virtual void Initialize(ChartSO.SpawnInfo info) => Initialize(info.idealTime, info.angle, info.speed, info.deltaAngle);
    public void Initialize(float idealTime, float angle, float speed, float deltaAngle)
    {
        this.idealTime = idealTime;
        this.angle = angle;
        this.speed = speed;
        this.deltaAngle = deltaAngle;
        UpdateVisual(0f);
    }


    private void Update() => UpdateVisual(Time.time - startTime);
    
    protected virtual void UpdateVisual(float timeLapsed) { }
    
    public bool InJudgementRange(float chartTime)
    {
        return idealTime - chartTime < judgementRange[(int) JudgementGrade.Count - 2];
    }
    
    public virtual JudgementGrade GetGradeAtTime(float chartTime)
    {
        float offset = idealTime - chartTime;
        for(int i = 0; i < (int) JudgementGrade.Count - 1; i++)
        {
            if (Mathf.Abs(offset) < judgementRange[i])
                return (JudgementGrade) i;
        }

        return JudgementGrade.Miss;
    }
}