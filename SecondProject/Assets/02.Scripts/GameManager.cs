using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public enum State 
    {
        Idle,
        LoadSongData,
        WaitUntilSongDataLoaded, // 비동기적으로 처리될 때를 대비하여 로드를 기다리는 상태.
        StartPlay,
        WaitUntilPlayFinished,
        DisplayScore,
        WaitForUser,
        
    }

    public State state;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.LoadSongData:
                break;
            case State.WaitUntilSongDataLoaded:
                break;
            case State.StartPlay:
                break;
            case State.WaitUntilPlayFinished:
                break;
            case State.DisplayScore:
                break;
            case State.WaitForUser:
                break;
            default:
                break;
        }
    }
}
