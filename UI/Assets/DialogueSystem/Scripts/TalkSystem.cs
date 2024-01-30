using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSystem : MonoBehaviour
{
    public int count = 5; // 대화의 총 개수
    public int check_count = 0; // 이 변수의 수치 조절 = 대화 진행;
    
    // 실행할 대화에 대한 정보
    // 1. 배열
    // 2. 리스트
    // 3. 큐
    // json, excel sheet 외부 파일
    
    // 이 스크립트에서는 가장 간단한 배열로 구현
    // 타이핑 텍스트 기능(코루틴)
    // 대화 속도 (2배, 3배...) : 타이핑 텍스트와 연계하거나, Time.timeScale을 수치로 조정하는걸로 게임 자체의 속도를 높히는 법도 존재한다.

    private string[] sentences; // 대화 문장
    private int[] image_cnt;
    
    public Text sentence_text; // 텍스트 UI 연결
    public Image[] character_icons;
    public Image textIcon;

    private void Start()
    {
        // 1. 대화 저장 공간에 대한 초기화 진행
        sentences = new string[count];
        image_cnt = new int[count];

        DialogueInitialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            check_count++;
        }

        showDialogue();
    }

    private void showDialogue()
    {
        textIcon.gameObject.SetActive(true);
        
        // 등장인물의 수만큼 반복을 진행해 오브젝트를 비활성화한다.
        for (int i = 0; i < 2; i++)
        {
            character_icons[i].gameObject.SetActive(false);
        }

        // 이미지 카운팅에 맞춰 아이콘의 이미지를 활성화한다.
        character_icons[image_cnt[check_count]].gameObject.SetActive(true);

        sentence_text.text = sentences[check_count];
    }

    // 대화 내용 작성
    private void DialogueInitialize()
    {
        sentences[0] = "집가고 싶다.";
        sentences[1] = "아무 것도 하고 싶지 않다.";
        sentences[2] = "살려주세요.";
        sentences[3] = "귀찮다.";
        sentences[4] = "인생";

        image_cnt[0] = 0;
        image_cnt[1] = 1;
        image_cnt[2] = 2;
        image_cnt[3] = 3;
        image_cnt[4] = 4;
    }
    
    
}
