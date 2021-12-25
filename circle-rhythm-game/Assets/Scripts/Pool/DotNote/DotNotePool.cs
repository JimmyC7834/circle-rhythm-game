using System.Collections;
using System.Collections.Generic;
using Game.Framework.Factory;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool/DotNote")]
public class DotNotePool : Game.Framework.Pool.ComponentPoolSO<DotNote>
{
    [SerializeField] private DotNoteFactory factory;
    

    public DotNote Request(ChartSO.SpawnInfo info)
    {
        DotNote newNote = base.Request();
        newNote.Initialize(info);
        return newNote;
    }

    public override IFactory<DotNote> Factory { get => factory as IFactory<DotNote>; set => factory = value as DotNoteFactory; }
}
