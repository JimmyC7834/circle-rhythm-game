using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Game;

public class LongNoteManager : MonoBehaviour
{
    [SerializeField] private LongNotePool pool;
    [SerializeField] private InputReader input;  
    private Queue<LongNote> showing;
    private List<LongNote> pressing;
    // private DotNote bufferNote;
    private ChartReader chartReader;
    [SerializeField] private int presses;

    public void Initialize(ChartReader chartReader)
    {
        showing = new Queue<LongNote>();
        pressing = new List<LongNote>();
        pool.PreWarm(10);

        input.dotHitEvent += HandlePlayerHitInput;
        input.dotReleaseEvent += HandlePlayerReleaseInput;
        this.chartReader = chartReader;
    }
    
    private void Update()
    {
        CheckAndHoldMissedNote();
        CheckAndHandleFinishedNote();
        CheckMarkPoint();
    }

    private void HandlePlayerHitInput()
    {
        presses++;
        UpdatePressingNotes();
        CheckHit();
    }
    
    private void HandlePlayerReleaseInput()
    {
        presses--;
        UpdatePressingNotes();
        CheckHit();
    }

    private void UpdatePressingNotes()
    {
        for (int i = Mathf.Max(0, pressing.Count - presses); i < pressing.Count; i++)
        {
            pressing[i].Press();
        }
        
        for (int i = 0; i < pressing.Count - presses; i++)
        {
            pressing[i].Miss();
        }
    }

    public void CheckMarkPoint()
    {
        for (int i = 0; i < pressing.Count; i++)
        {
            if (chartReader.ChartTime - pressing[i].prevMarkPoint >= 60f/chartReader.bpm)
            {
                pressing[i].MarkPoint(chartReader.ChartTime);
            }
        }
    }
    
    private void CheckHit()
    {
        if (showing.Count == 0) return;
        
        if (showing.Peek().InJudgementRange(chartReader.ChartTime))
        {
            Debug.Log($"{showing.Peek().GetGradeAtTime(chartReader.ChartTime)}");
            showing.Peek().Press();
            pressing.Add(showing.Dequeue());
        }
    }
    
    private void CheckAndHoldMissedNote()
    {
        if (showing.Count == 0) return;
        
        if (showing.Peek().InJudgementRange(chartReader.ChartTime) && showing.Peek().GetGradeAtTime(chartReader.ChartTime) == Note.JudgementGrade.Miss)
        {
            showing.Peek().Miss();
            Debug.Log(("LongNoteMissed!"));
            pressing.Add(showing.Dequeue());
        }
    }
    
    private void CheckAndHandleFinishedNote()
    {
        if (pressing.Count == 0) return;

        pressing.ForEach(note =>
        {
            if (note.Ended(chartReader.ChartTime))
                ReturnNote(note);
        });
        
        pressing = pressing.Where(note => !note.Ended(chartReader.ChartTime)).ToList();
    }

    private void ReturnNote(LongNote note)
    {
        pool.Return(note);
    }
    
    public void SpawnNote(ChartSO.SpawnInfo info)
    {
        showing.Enqueue(pool.Request(info));
    }
}
