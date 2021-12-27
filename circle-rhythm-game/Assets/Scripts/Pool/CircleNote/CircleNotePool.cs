using System.Collections;
using System.Collections.Generic;
using Game.Framework.Factory;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool/CircleNote")]
public class CircleNotePool : Game.Framework.Pool.ComponentPoolSO<CircleNote>
{
    [SerializeField] private CircleNoteFactory factory;

    public CircleNote Request(ChartSO.SpawnInfo info)
    {
        CircleNote newNote = base.Request();
        newNote.Initialize(info);
        return newNote;
    }

    public override IFactory<CircleNote> Factory { get => factory; set => factory = value as CircleNoteFactory; }
}