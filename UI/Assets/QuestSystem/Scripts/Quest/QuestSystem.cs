using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 퀘스트 데이터베이스를 통해 퀘스트를 처리할 메인 시스템
/// </summary>
public class QuestSystem : MonoBehaviour
{
    #region Singleton
    
    private static QuestSystem instance;
    private static bool isApplicationQuitting;

    public static QuestSystem Instance
    {
        get
        {
            if (!isApplicationQuitting && instance == null)
            {
                instance = FindObjectOfType<QuestSystem>();
                // Find : 오브젝트 이름을 통해서 찾기
                // FindObjectOfType : 스크립트 이름으로 찾기
                // FindGameObjectWithTag : 태그를 이용해 찾기
                // FindGameObjectsWithTag : 오브젝트 배열(묶음)으로부터 태그 검색

                if (instance == null)
                {
                    // 퀘스트 시스템이란 이름으로 Quest System 컴포넌트를 추가해 생성한다.
                    instance = new GameObject("Quest System").AddComponent<QuestSystem>();
                }
                
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
        
    }
    #endregion

    #region Quest List

    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();
    private List<Quest> activeAchievements = new List<Quest>();
    private List<Quest> completeAchievements = new List<Quest>();
    
    #endregion

    #region Quest Database

    private QuestDB quest_DB;
    private QuestDB achievementDatabase;

    #endregion

    #region Delegate & Event

    public delegate void QuestActiveHandler(Quest quest);
    public delegate void QuestCompleteHandler(Quest quest);
    public delegate void QuestCancelHandler(Quest quest);

    public event QuestActiveHandler QuestActived;
    public event QuestCompleteHandler QuestCompleted;
    public event QuestCancelHandler QuestCanceled;

    public event QuestActiveHandler AchievementActived;
    public event QuestCompleteHandler AchievementCompleted;

    #endregion

    #region ReadOnlyList

    public IReadOnlyList<Quest> ActivedQuests => activeQuests;
    public IReadOnlyList<Quest> CompletedQuests => completedQuests;
    public IReadOnlyList<Quest> ActiveAchievementsQuests => activeAchievements;
    public IReadOnlyList<Quest> CompleteAchievements => completeAchievements;

    #endregion

    private void Awake()
    {
        quest_DB = Resources.Load<QuestDB>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDB>("AchievementDatabase");
    }

    public Quest Active(Quest quest)
    {
        var new_Quest = Instantiate(quest);

        if (new_Quest is Achievement)
        {
            new_Quest.completeHandler += OnAchievementCompleted;
            activeAchievements.Add(new_Quest);
            new_Quest.Active();
            AchievementActived?.Invoke(new_Quest);
        }
        else
        {
            new_Quest.completeHandler += OnQuestCompleted;
            new_Quest.cancelHandler += OnQuestCanceled;
            activeQuests.Add(new_Quest);
            new_Quest.Active();
            QuestActived?.Invoke(new_Quest);
        }
        
        return new_Quest;
    }

    private void OnQuestCanceled(Quest quest)
    {
        
    }

    private void OnQuestCompleted(Quest quest)
    {
        
    }

    private void OnAchievementCompleted(Quest quest)
    {
        
    }

    public void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }
}
