using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Sample : MonoBehaviour
{
    #region IMGUI
    // Unity IMNGUI(Immediate Mode GUI)
    // 코드에 의해 구동되는 GUI 시스템
    // 특징) 유니티에서 제공해주는 함수 OnGUI()를 통해 호출되고 구동된다.
    
    // 일반적인 유니티의 UI 시스템 (UGUI : 게임 오브젝트 기반)과는 별개의 시스템으로 
    // Start, Update와 다른 별도의 영역에서 작업이 처리된다.
    
    // 해당 기능의 목적
    // 인게임 디버깅 디스플레이
    // 스크립트 컴포넌트를 위한 인스펙터 제작
    // 유니티 확장을 위한 에디터 윈도우 생성
    #endregion

    private void OnGUI() // 이미지가 바로 프로그램 내에 렌더링되므로, 성능이 떨어진다.
    {
        // 박스는 GUI의 배경을 작업할 때 많이 사용된다.
        GUI.Box(new Rect(10, 10, 100, 90), "MY MENU");
        
        // 버튼에 대한 설계
        // 버튼을 눌렀을 경우를 작업하기에 if문을 사용
        if (GUI.Button(new Rect(20, 40, 80, 20), "Scene 1")) ;
        {
            //Application.loadedLevel(1); // 옛날 방식
            SceneManager.LoadScene(2); // 2번 씬으로 이동
        }
    }
}
