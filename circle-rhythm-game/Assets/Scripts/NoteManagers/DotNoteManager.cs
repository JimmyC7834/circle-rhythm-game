using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game;

public class DotNoteManager : MonoBehaviour
{
    [SerializeField] private DotNotePool pool;
    [SerializeField] private InputReader input;  
    private Queue<DotNote> showing;
    // private DotNote bufferNote;
    private ChartReader chartReader;

    public void Initialize(ChartReader chartReader)
    {
        showing = new Queue<DotNote>();
        pool.PreWarm(10);

        input.dotHitEvent += HandlePlayerInput;
        this.chartReader = chartReader;
    }
    
    private void Update()
    {
        // CheckAndSpawnBufferNote();
        CheckAndRemoveMissedNote();
    }

    private void HandlePlayerInput()
    {
        if (showing.Count == 0) return;

        if (showing.Peek().InJudgementRange(chartReader.ChartTime))
        {
            Debug.Log($"{showing.Peek().GetGradeAtTime(chartReader.ChartTime)}");
            ReturnNote(showing.Dequeue());
        }
    }

    private void CheckAndRemoveMissedNote()
    {
        if (showing.Count == 0) return;
        
        if (showing.Peek().InJudgementRange(chartReader.ChartTime) && showing.Peek().GetGradeAtTime(chartReader.ChartTime) == Note.JudgementGrade.Miss)
        {
            ReturnNote(showing.Dequeue());
            Debug.Log(Note.JudgementGrade.Miss);
        }
    }

    // private void CheckAndSpawnBufferNote()
    // {
    //     if (bufferNote == null) return;
    //
    //     if (chartReader.ChartTime >= bufferNote.spawnTime)
    //         SpawnBufferNote();
    // }

    private void ReturnNote(Note note)
    {
        pool.Return(note as DotNote);
    }
    
    // public void BufferNextNote(ChartSO.SpawnInfo info)
    // {
    //     bufferNote = pool.Request(info);
    //     bufferNote.gameObject.SetActive(false);
    // }
    //
    // private void SpawnBufferNote()
    // {
    //     showing.Enqueue(bufferNote);
    //     bufferNote.gameObject.SetActive(true);
    //     bufferNote = null;
    // }

    public void SpawnNote(ChartSO.SpawnInfo info)
    {
        showing.Enqueue(pool.Request(info));
    }
}