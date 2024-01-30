using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 퀘스트에서의 작업 내역을 만들기 위한 가장 기본적인 틀
// 월간, 주간, 일일 퀘스트 등 존재하기에 큰 틀을 제작하고 상속시킨다.
// 프로그램 내에서 만들어 줄 데이터(Scriptable Object)
// 데이터 덩어리이다.

public enum State
{
    Inactive,
    Running,
    Complete
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]

// ScriptableObject : 유니티 내에서 연결해서 사용하는 것이 Monobehaviour이라면 ScriptableObject를 연결 시, 에디터 내에서 해당 형태의 클래스로 설계된 데이터를 오브젝트처럼 생성해서 사용할 수 있다.
public class Task : ScriptableObject
{
    [Header("ID")] // 작업을 분류하는 기준 (아이디 코드 / 작업의 설명
    [SerializeField] private string id;
    [SerializeField] private string description;

    [Header("Settings")] // 작업에 필요한 기본 설정 (성공 횟수)
    [SerializeField] private int successCount;
    [SerializeField] private InitialSuccessValue initialSuccessValue;
    [SerializeField] private bool ReceiveValue;


    [Header("Action")] // 작업에 대한 Action 
    [SerializeField] private TaskAction action;

    [Header("Target")] // 작업에 대한 타겟
    [SerializeField] private Target[] targets;

    [Header("Category")] // 작업 카테고리
    [SerializeField] private Category category;

    private State state; // 작업 상태
    private int current_success; // 현재의 수치
    
    // 상태 변화에 따라 처리할 이벤트 설계(delegate)
    public delegate void OnStateChanged(Task task, State current_State, State pre_state);
    public delegate void OnCompleted(Task task, int success, int pre_success);

    public event OnStateChanged onStateChanged; // 상태 변화 시, 처리할 이벤트
    public event OnCompleted onCompleted; // 퀘스트 완료 시, 처리할 이벤트
    
    
    // 작업 성공 자체에 대한 프로퍼티(읽기 전용)
    // 설명, 코드, 성공 횟수에 대한 접근 프로퍼티
    public int Success { get; private set; } // private set이 붙는 경우 읽기 전용
    public string Description => description;
    public string Id => id;
    public Category Category => category;

    public bool IsComplete => State == State.Complete;
    public Quest Owner { get; private set; }
    
    public State State
    {
        get => state;
        set
        {
            var pre_state = state;
            state = value;
            onStateChanged?.Invoke(this, pre_state, value);
            
            // ? null 체크연산자 : null이 아니라면 참조하고, null이면 null로써 처리하게하는 연산자 기호.
            // ?? : null이라면 오른쪽 값으로 처리한다.
            // delegate : 대리자를 의미하며, 메소드에 대한 포인터를 저장하며, 주로 이벤트에 대한 처리를 진행할 때 사용된다.
            // Invoke : 별도의 스레드에서 하나의 컨트롤 개체에 접근하려 할 때, 서로 다른 스레드가 하나의 컨트롤 개체에 접근하는 것을 방지한다.
        }
    }

    public int Currene_Success
    {
        get => current_success;
        set
        {
            int pre_success = current_success;
            current_success = Mathf.Clamp(value, 0, successCount);

            if (current_success != pre_success)
            {
                State = current_success == successCount ? State.Complete : State.Running;
                
                onCompleted?.Invoke(this, current_success, pre_success);
            }
        }
    }

    /// <summary>
    /// 성공 횟수를 전달해 Success 프로퍼티에 적용한다.
    /// </summary>
    /// <param name="s_count"></param>
    public void Receive(int s_count)
    {
        Success = action.Run(this, Success, s_count);
    }

    /// <summary>
    /// LINQ(쿼리 언어) 형태로 작업에 대한 요청 설계
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool IsTarget(string category, object target) => Category == Category && targets.Any(x => x.IsTarget(target)) && (!IsComplete || (IsComplete && ReceiveValue)); 
    // 묶음의 형태에서 작업할 수 있는 LINQ Any 기능
    // 해당 타겟들 중에서 값이 타겟과 동일한지 체크한다.
    // Any : 하나라도 맞으면 true
    // All : 전체 조사

    public void Start()
    {
        State = State.Running;
        if (initialSuccessValue)
        {
            current_success = initialSuccessValue.GetValue(this);
        }
    }

    public void End()
    {
        onStateChanged = null;
        onCompleted = null;
    }

    public void Complete()
    {
        Currene_Success = successCount;
    }

    public void Setup(Quest owner)
    {
        Owner = owner;
    }
}
