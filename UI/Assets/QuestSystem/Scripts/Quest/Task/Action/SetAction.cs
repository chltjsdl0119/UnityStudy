using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/SetAction", fileName = "Set Action")]

public class SetAction : TaskAction
{
    public override int Run(Task task, int success, int s_count)
    {
        return success;
    }
}