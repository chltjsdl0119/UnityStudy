using System;

public interface IWorkflow<T>
    where T : Enum
{
    T ID { get; }
    
    bool CanExcute { get; }
    int Current { get; }

    T MoveNext();
    void Reset();
}