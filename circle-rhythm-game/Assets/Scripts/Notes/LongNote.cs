using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LongNote : Note
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] protected float duration;
    [SerializeField] protected Color pressingColor;
    [SerializeField] protected Color missingColor;
    protected override float[] judgementRange => new[] { .2f, .25f, .35f };
    protected float Radius2(float timeLapsed) => Mathf.Max(0, radius - speed * timeLapsed + duration * speed);
    protected Vector2 Position2(float timeLapsed) => new Vector2(
        Mathf.Cos(Theta(timeLapsed) * Mathf.Deg2Rad) * Radius2(timeLapsed), 
        Mathf.Sin(Theta(timeLapsed) * Mathf.Deg2Rad) * Radius2(timeLapsed)
    );

    protected bool missed;
    protected bool missing;
    public float prevMarkPoint;
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Initialize(ChartSO.SpawnInfo info)
    {
        base.Initialize(info);
        duration = info.duration;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        nodeType = NoteType.Long;
    }

    protected override void UpdateVisual(float timeLapsed)
    {
        transform.position = Position(timeLapsed);
        lineRenderer.SetPositions(new Vector3[]
        {
            Position(timeLapsed),
            Position2(timeLapsed)
        });

        if (missing)
        {
            lineRenderer.startColor = missingColor;
            lineRenderer.endColor = missingColor;
        }
        else
        {
            lineRenderer.startColor = pressingColor;
            lineRenderer.endColor = pressingColor;
        }
    }

    public void MarkPoint(float chartTime)
    {
        prevMarkPoint = chartTime;
        Debug.Log($"markpoint");
    }
    
    public bool Ended(float chartTime) => chartTime - idealTime > duration;
    
    public void Miss()
    {
        missed = true;
        missing = true;
    }
    
    public void Press()
    {
        missing = false;
    }
}
