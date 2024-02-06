using System;
using System.Linq; // Collection의 다양한 질의(Query) 기능들을 포함하는 네임스페이스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))] // Add Component가 귀찮을 때 쓴다.
public class MusicPlayManager : MonoBehaviour
{
    public static MusicPlayManager instance;

    public float speedGain = 1.0f;

    public bool isPlaying;
    private VideoPlayer _videoPlayer;
    private Queue<NoteData> _queue;
    private float _timeMark;

    private Dictionary<KeyCode, NoteSpawner> _spawners;
    [SerializeField] private List<NoteSpawner> _spawnersList;

    private void Awake()
    {
        instance = this;
        _videoPlayer = GetComponent<VideoPlayer>();
        _spawners = new Dictionary<KeyCode, NoteSpawner>();
        
        foreach (var spawner in _spawnersList)
        {
            _spawners.Add(spawner.key, spawner);
        }
    }

    public void StartMusicPlay()
    {
        _queue = new Queue<NoteData>(SongDataLoader.dataLoaded.noteDatum.OrderBy(x => x.time));
        _videoPlayer.clip = SongDataLoader.clipLoaded;
        _videoPlayer.Play();
        _timeMark = Time.time;
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying == false)
            return;

        while (_queue.Count > 0)
        {
            if (_queue.Peek().time <= Time.time - _timeMark)
            {
                Debug.Log($"[MusicPlayManager] : Spawned Note {_queue.Dequeue().key}");
            }
            else
            {
                break;
            }
        }
            

        if (_queue.Count <= 0)
        {
            isPlaying = false;
        }
    }
}
