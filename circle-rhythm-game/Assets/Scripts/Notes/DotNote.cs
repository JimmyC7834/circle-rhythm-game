using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotNote : Note
{
    protected virtual float[] JudgementRange => new[] { .1f, .25f, .35f, .5f };

    private void OnEnable()
    {
        startTime = Time.time;
        transform.position = Position(0);
    }

    private void FixedUpdate()
    {
        transform.position = Position(Time.time - startTime);
    }
}
