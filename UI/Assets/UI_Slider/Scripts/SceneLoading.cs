using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Slider progress_bar; // 작업 수치를 보여줄 바
    public Text progress_text; // 작업에 대한 텍스트 처리
    public string SceneName; // 이동할 씬의 이름

    private float time;

    private void Start()
    {
        // 코루티 작동
        StartCoroutine(AsyncLoadScene());
    }

    // 코루틴 : 유니티에서 코루틴은 실행을 일시 정지하고 제어를 유니티에 반환한다.
    // 그 후, 중단한 부분에서 다음 프레임을 이어서 작업할 수 있는 메소드이다.
    // 주로 시간에 흐름에 따른 이벤트, 절차상의 애니메이션을 위한 메소드 콜 작업에서 많이 사용된다.
    
    // 코루틴의 데이터 형대 IEnumerator(간단한 반복을 지원해주는 C# 컬렉션)
    
    // 동기(Synchronous) 단순 설계가 잘된다.
    // 1. 요청을 보낸다.
    // 2. 응답을 받아야 다음 동작이 진행된다. 이 때 그 작업을 처리할 동안 대기 상태가 된다.
    
    // 비동기(ASynchronous) 여러 작업을 동시에 처리 가능, 최적화 코드
    // 작업을 병렬적으로 처리
    // 1. 요청을 보낸다.
    // 2. 응답 여부와 상관없이 다음 동작이 진행된다.
    // 3. 동작 진행 시간동안, 다른 동작을 작업할 수 있어 효율적인 작업이 가능하다.
    // 4. 처리 순서를 보장하기 위해 콜백의 복잡도가 높아진다.
    // -> 여러 작업을 동시에 처리 가능, 최적화 코드, 구성이 어렵고, 실수가 날 일이 많다.
    
    IEnumerator AsyncLoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            time += Time.time; // 실제 시간

            progress_bar.value = time / 100;

            progress_text.text = $"{progress_bar.value * 100 :0}%"; // 서식지정자로 소수점 출력 X

            if (time > 100)
            {
                progress_text.text = $"ㅋㅋ";
                if (Input.GetKeyDown(KeyCode.Return)) // 엔터키 입력 받기
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
        
            yield return null;
        }
        
        // yield return은 특정 조건이 다 끝나면 다시 돌아와서 실행되는게 코루틴인데 이 때의 그 특정 조건의 시작점
        
        // yield return new WaitForEndOfFrame(); : 모든 렌더링 작업이 끝날 때까지 대기
        // yield return StartCoroutine(string); : 입력한 다른 코루틴이 끝날 때까지 대기
        // yield return null; : 다음 프레임까지 대기
        // yield return new WaitForSeconds(float); : 입력한 초만큼 대기
        // yield break; : 코루틴 종료
    }
}
