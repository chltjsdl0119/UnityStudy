using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    public static GameManager Instance;

    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
    }
}
