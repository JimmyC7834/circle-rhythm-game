using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleNote : Note
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int nodeCount;
    protected override float[] judgementRange => new[] { .1f, .25f, .35f };
    public override float spawnTime => idealTime - (radius - .5f) / speed;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        nodeType = NoteType.Circle;
        lineRenderer.positionCount = nodeCount;
    }
    
    protected override void UpdateVisual(float timeLapsed)
    {
        Vector3[] nodes = new Vector3[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            nodes[i] = new Vector3(
                Mathf.Cos(Mathf.PI * 2/nodeCount * i) * Radius(timeLapsed),
                Mathf.Sin(Mathf.PI * 2/nodeCount * i) * Radius(timeLapsed)
            );
        }
        
        lineRenderer.SetPositions(nodes);
    }
}
