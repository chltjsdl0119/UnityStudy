using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/String", fileName = "S_Target_")]
public class StringTarget : Target
{
    [SerializeField] private string value;

    public override object Value => value;
    
    public override bool IsTarget(object target)
    {
        string targetString = target as string;

        if (targetString != null) // 비어있지 않다면? 같다.
        {
            return value == targetString;
        }

        return false;
    }
}