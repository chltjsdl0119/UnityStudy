using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/PositiveAction", fileName = "Positive Action")]

public class PositiveAction : TaskAction
{
    public override int Run(Task task, int success, int s_count)
    {
        return s_count > 0 ? success + s_count : success;
    }
}