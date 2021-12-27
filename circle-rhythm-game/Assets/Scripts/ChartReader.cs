using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartReader : MonoBehaviour
{
    [SerializeField] private DotNoteManager dotNoteManager;
    [SerializeField] private LongNoteManager longNoteManager;
    [SerializeField] private CircleNoteManager circleNoteManager;
    private Queue<ChartSO.SpawnInfo> waitList;
    private float radius;
    private float startTime;
    private bool started;
    private bool initialized;
    public float ChartTime => Time.time - startTime;
    public float bpm;
    
    public void LoadChart(ChartSO chart)
    {
        waitList = new Queue<ChartSO.SpawnInfo>();
        dotNoteManager.Initialize(this);
        longNoteManager.Initialize(this);
        circleNoteManager.Initialize(this);
    
        radius = chart.radius;
        bpm = chart.bpm;
        for (int i = 0; i < chart.spawnInfos.Length; i++)
            waitList.Enqueue(chart.spawnInfos[i]);
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
    }

    private void CheckAndRequestNextNote()
    {
        if (waitList.Count == 0) return;
    
        ChartSO.SpawnInfo info = waitList.Peek();
        if (ChartTime >= info.SpawnTime(radius))
        {
            NextNote();
        }
    }
    
    private void NextNote()
    {
        ChartSO.SpawnInfo info = waitList.Dequeue();
        switch (info.noteType)
        {
            case Note.NoteType.Dot:
                dotNoteManager.SpawnNote(info);
                break;
            case Note.NoteType.Long:
                longNoteManager.SpawnNote(info);
                break;
            case Note.NoteType.Circle:
                circleNoteManager.SpawnNote(info);
                break;
        }
    }
}
