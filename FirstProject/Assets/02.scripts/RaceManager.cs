using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // 싱글톤 패턴
    // 해당 타입의 객체가 단 하나만 존재할 때 클래스를 통해 접근을 용이하게 만들기 위한 형태
    public static RaceManager instance;

    public Horse lead => horses.OrderByDescending(x => x.totalDistance).FirstOrDefault();
    
    public List<Horse> horses = new List<Horse>();
    public List<Horse> arrived = new List<Horse>();

    [SerializeField] private Transform _standPoint1st;
    [SerializeField] private Transform _standPoint2nd;
    [SerializeField] private Transform _standPoint3rd;

    private void Awake()
    {
        instance = this;
    }
    
    public void Resister(Horse horse)
    {
        horses.Add(horse);
    }
    
    public void StartRace()
    {
        foreach (Horse horse in horses)
        {
            horse.doMove = true;
        }
    }

    public void AddArrived(Horse horse)
    {
        arrived.Add(horse);

        if (arrived.Count == horses.Count)
        {
            FinishRace();
        }
    }


    public void FinishRace()
    {
        arrived[0].transform.position = _standPoint1st.transform.position;
        arrived[1].transform.position = _standPoint2nd.transform.position;
        arrived[2].transform.position = _standPoint3rd.transform.position;
    }
}