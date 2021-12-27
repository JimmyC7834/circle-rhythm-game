using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private ChartReader chartReader;
    [SerializeField] private ChartSO chart;

    private void Awake()
    {
        InitializeGamePlay();
    }

    private void InitializeGamePlay()
    {
        chartReader.LoadChart(chart);
        chartReader.StartChart();
    }
}
