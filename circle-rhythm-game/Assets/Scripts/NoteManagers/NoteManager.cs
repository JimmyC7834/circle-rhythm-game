using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Game;
using Game.Framework.Pool;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private DotNotePool dotNotePool;
    [SerializeField] private CircleNotePool circleNotePool;
    [SerializeField] private InputReader input;  
    private Queue<ChartSO.SpawnInfo> waitList;
    private Queue<Note> showing;
    private Note bufferNote;
    private float radius;
    private float startTime;
    private bool started = false;
    private float chartTime => Time.time - startTime;
    
    private void Awake()
    {
        waitList = new Queue<ChartSO.SpawnInfo>();
        showing = new Queue<Note>();
        dotNotePool.PreWarm(10);
        circleNotePool.PreWarm(10);

        input.dotHitEvent += HandlePlayerInput;
    }

    public void LoadChart(ChartSO chart)
    {
        waitList.Clear();
        showing.Clear();

        radius = chart.radius;
        for (int i = 0; i < chart.spawnInfos.Length; i++)
        {
            waitList.Enqueue(chart.spawnInfos[i]);
            Debug.Log($"chart: {i}");
        }

        if (waitList.Count != 0)
            BufferNextNote();
    }

    public void StartChart()
    {
        startTime = Time.time;
        started = true;
    }

    private void Update()
    {
        if (!started) return;
        CheckAndRequestNextNote();
        CheckAndRemoveMissedNote();
    }

    private void HandlePlayerInput()
    {
        if (showing.Count == 0) return;

        if (showing.Peek().InJudgementRange(chartTime))
        {
            Debug.Log($"{showing.Peek().GetGradeAtTime(chartTime)}");
            ReturnNode(showing.Dequeue());
        }
    }

    private void CheckAndRemoveMissedNote()
    {
        if (showing.Count == 0) return;
        
        if (showing.Peek().InJudgementRange(chartTime) && showing.Peek().GetGradeAtTime(chartTime) == Note.JudgementGrade.Miss)
        {
            ReturnNode(showing.Dequeue());
            Debug.Log(Note.JudgementGrade.Miss);
        }
    }

    private void CheckAndRequestNextNote()
    {
        if (waitList.Count == 0) return;

        if (chartTime >= bufferNote.spawnTime)
            NextNote();
    }

    private void ReturnNode(Note note)
    {
        switch (showing.Peek().nodeType)
        {
            case Note.NoteType.Dot:
                dotNotePool.Return(note as DotNote);
                break;
            case Note.NoteType.Long:
                dotNotePool.Return(note as DotNote);
                break;
            case Note.NoteType.Circle:
                circleNotePool.Return(note as CircleNote);
                break;
        }
    }
    
    private void BufferNextNote()
    {
        ChartSO.SpawnInfo info = waitList.Dequeue();
        switch (info.noteType)
        {
            case Note.NoteType.Dot:
                bufferNote =  dotNotePool.Request(waitList.Dequeue());
                break;
            case Note.NoteType.Long:
                bufferNote =  dotNotePool.Request(waitList.Dequeue());
                break;
            case Note.NoteType.Circle:
                bufferNote =  circleNotePool.Request(waitList.Dequeue());
                break;
            // default:
            //     bufferNote =  dotNotePool.Request(waitList.Dequeue());
        }

        bufferNote.gameObject.SetActive(false);
    }

    private void NextNote()
    {
        showing.Enqueue(bufferNote);
        bufferNote.gameObject.SetActive(true);
        BufferNextNote();
    }
}
