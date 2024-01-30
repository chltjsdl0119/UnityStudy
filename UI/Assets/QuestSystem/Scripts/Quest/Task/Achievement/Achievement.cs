using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Achievement", fileName = "Achievement_")]
public class Achievement : Quest
{
    public override bool IsCancelable => false;

    public override void Cancel()
    {
        Debug.LogAssertion("없적 취소 불가 상태이다.");
    }
}
