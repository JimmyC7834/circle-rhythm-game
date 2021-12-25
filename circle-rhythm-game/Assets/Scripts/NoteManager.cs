using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private DotNotePool dotNotePool;
    private float radius;
    private Queue<ChartSO.SpawnInfo> waitList;
    private Queue<Note> showing;
    private float startTime;
    private bool started = false;
    private float ChartTime => Time.time - startTime;
    
    private void Awake()
    {
        waitList = new Queue<ChartSO.SpawnInfo>();
        showing = new Queue<Note>();
        dotNotePool.PreWarm(10);
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

    private void CheckAndRemoveMissedNote()
    {
        if (showing.Count == 0) return;
        
        Debug.Log(($"{showing.Peek().GetGradeAtTime(ChartTime)}"));
        if (showing.Peek().transform.position.magnitude == 0 && showing.Peek().GetGradeAtTime(ChartTime) == Note.JudgementGrade.Miss)
        {
            ReturnNode(showing.Dequeue());
            Debug.Log("Missed!");
        }
    }

    private void CheckAndRequestNextNote()
    {
        if (waitList.Count == 0) return;

        ChartSO.SpawnInfo info = waitList.Peek();
        if (ChartTime >= info.idealTime - radius/info.speed)
        {
            NextNote();
        }
    }

    private void ReturnNode(Note note)
    {
        dotNotePool.Return(note as DotNote);
    }
    
    private void NextNote()
    {
        showing.Enqueue(dotNotePool.Request(waitList.Dequeue()));
    }
}
