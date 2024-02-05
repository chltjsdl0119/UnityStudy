using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public static class SongDataLoader
{
    public static bool isLoaded { get; private set; }
    
    public static SongData dataLoaded;
    public static VideoClip clipLoaded;
     
    public static void Load(string songName) // Resources 클래스는 권장되는 클래스가 아니다. 게임 시작부터 끝까지 존재해야하는 에셋이라면 괜찮다.
    {
        isLoaded = false;
        dataLoaded = null;
        clipLoaded = null;
        
        dataLoaded = JsonUtility.FromJson<SongData>(Resources.Load<TextAsset>($"SongDatum/{songName}").ToString());
        clipLoaded = Resources.Load<VideoClip>($"SongClips/{songName}");

        isLoaded = true;
    }
}
