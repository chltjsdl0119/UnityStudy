using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 시작 안된 상태, 진행 중, 완료, 포기, 완료 처리
public enum Q_State
{
    Inactive,
    Running,
    Complete,
    Cancel,
    OnComplete
}

[CreateAssetMenu(menuName = "Quest/Quest", fileName = "Quest_")]
public class Quest : ScriptableObject
{
    public delegate void OnCompleted(Quest quest, Task task, int current_success, int pre_success);

    public delegate void CompleteHandler(Quest quest);
    public delegate void CancelHandler(Quest quest);
    public delegate void TaskGroupHandler(Quest quest, TaskGroup current_TaskGroup, TaskGroup pre_TaskGroup);

    public event OnCompleted onCompleted;
    public event CompleteHandler completeHandler;
    public event CancelHandler cancelHandler;
    public event TaskGroupHandler taskGroupHandler;
    
    [Header("Category")]
    [SerializeField] private Category category;
    [SerializeField] private Sprite icon;
    
    [Header("ID")]
    [SerializeField] private string id;
    [SerializeField] private string ingame_id;
    [SerializeField, TextArea] private string description;

    [Header("Settings")]
    [SerializeField] private bool auto_complete;
    
    [Header("Task")]
    [SerializeField] private TaskGroup[] taskGroups;

    [Header("Reward")] 
    [SerializeField] IReadOnlyList<Reward> rewards;

    [Header("Condition")] 
    [SerializeField] private Condition[] acceptionConditions;
    [SerializeField] private Condition[] cancelCondition;
    
    private int idx;
    private bool isCancelable;

    public Category Category => category;

    public Sprite Icon => icon;

    public string Id => id;

    public string Ingame_Id => ingame_id;

    public string Description => description;
    
    public Q_State State { get; private set; }
    public TaskGroup Current_Group => taskGroups[idx];
    public IReadOnlyList<TaskGroup> TaskGroups => taskGroups;
    public IReadOnlyList<Reward> Rewards => rewards;
    
    // 상태에 따른 프로퍼티
    public bool InActive => State != Q_State.Inactive;
    public bool InComplete => State == Q_State.Complete;
    public bool InOnComplete => State == Q_State.OnComplete;
    public bool InCancel => State == Q_State.Cancel;

    public bool IsAcceptable => acceptionConditions.All(x => x.IsPass(this));
    public virtual bool IsCancelable => isCancelable && cancelCondition.All(x => x.IsPass(this));


    public void Active()
    {
        Debug.Assert(!InActive, "이 퀘스트는 이미 활성화되어 있다.");

        foreach (var taskGroup in taskGroups)
        {
            // Setup 진행
            taskGroup.Setup(this);
            // 그룹 내에서 작업 마다에 작업을 진행
            foreach (var task in taskGroup.Tasks)
            {
                task.onCompleted += OnTaskCompleted;
            }
        }

        State = Q_State.Running;
        Current_Group.Start();
    }

    // OnComplete 이벤트에 연결해줄 함수 
    private void OnTaskCompleted(Task task, int current_success, int pre_success) =>
        onCompleted?.Invoke(this, task, current_success, pre_success);

    public void Receive(string category, object target, int S_count)
    {
        if (InComplete)
        {
            return;
        }
        
        Current_Group.Receive(category, target, S_count);

        // 모든 작업이 마무리된 상태에서
        if (Current_Group.IsComplete)
        {
            // idx + 1이 그룹의 길이와 같다면
            if (idx + 1 == taskGroups.Length)
            {
                State = Q_State.OnComplete;
                if (auto_complete)
                {
                    Complete();
                }
            }
            else
            {
                // 현재 위칭의 그룹에 대한 설정
                // 그 후 인덱스 1 증가
                var pre_Group = taskGroups[idx++];
                pre_Group.End();
                Current_Group.Start();
                taskGroupHandler?.Invoke(this, Current_Group, pre_Group);
            }
        }
        else
        {
            State = Q_State.Running;
        }
    }

    public void Complete()
    {
        foreach (var taskGroup in taskGroups)
        {
            taskGroup.Complete();
        }

        State = Q_State.Complete;


        foreach (var reward in rewards)
        {
            reward.Give(this);
        }
        
        completeHandler?.Invoke(this);

        onCompleted = null;
        cancelHandler = null;
        completeHandler = null;
        taskGroupHandler = null;
    }
    
    /// <summary>
    /// 일반 상속애소 다른 클래스를 만들 때, 선언(abstract)이 아닌 만들어진 값에 대해서 오버라이딩을 진행할 경우 virtual을 통해 자식 쪽의 값을 연결할 수 있게 처리해준다.
    /// </summary>
    public virtual void Cancel()
    {
        State = Q_State.Cancel;
        cancelHandler?.Invoke(this);
    }
}
