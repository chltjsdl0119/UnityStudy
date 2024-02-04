using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SongDataMaker : MonoBehaviour
{
    public SongData songData;
    public VideoPlayer videoPlayer;

    private KeyCode[] _keys = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K, KeyCode.L };

    private bool _isRecoding;
    private void Update()
    {
        if (_isRecoding == false)
            return;
        
        
        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKeyDown(_keys[i]))
            {
                songData.noteDatum.Add(CreateNoteData(_keys[i]));
            }
        }
        
        
    }

    public void StartRecord()
    {
        if (_isRecoding)
            return;

        _isRecoding = true;
        songData = new SongData();
        songData.name = videoPlayer.clip.name;
        videoPlayer.Play();
    }
    
    public void StopRecord()
    {
        if (_isRecoding == false)
            return;
        
        videoPlayer.Stop();
        SaveRecord();
    }

    private void SaveRecord()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanelInProject("Save Song Data", songData.name, "json", "");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(songData));
    }

    public NoteData CreateNoteData(KeyCode key)
    {
        NoteData noteData = new NoteData()
        {
            key = key,
            time = (float)System.Math.Round(videoPlayer.time, 2)
        };
        
        Debug.Log($"[SongDataMaker, {key}, {noteData.time}");

        return noteData;
    }
}
