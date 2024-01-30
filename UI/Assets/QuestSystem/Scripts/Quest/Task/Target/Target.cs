using UnityEngine;

public abstract class Target : ScriptableObject
{
    public abstract object Value { get; }
    
    // 타겟이 Task에서 지정한 타겟인지를 확이하는 용도
    public abstract bool IsTarget(object target);
}