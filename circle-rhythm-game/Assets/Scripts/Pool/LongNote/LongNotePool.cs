using System.Collections;
using System.Collections.Generic;
using Game.Framework.Factory;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool/LongNote")]
public class LongNotePool : Game.Framework.Pool.ComponentPoolSO<LongNote>
{
    [SerializeField] private LongNoteFactory factory;
    

    public LongNote Request(ChartSO.SpawnInfo info)
    {
        LongNote newNote = base.Request();
        newNote.Initialize(info);
        return newNote;
    }

    public override IFactory<LongNote> Factory { get => factory; set => factory = value as LongNoteFactory; }
}