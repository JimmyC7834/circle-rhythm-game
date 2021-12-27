using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotNote : Note
{
    protected override float[] judgementRange => new[] { .1f, .25f, .35f};
    
    protected override void OnEnable()
    {
        base.OnEnable();
        nodeType = NoteType.Dot;
    }

    protected override void UpdateVisual(float timeLapsed)
    {
        transform.position = Position(timeLapsed);
    }
}
