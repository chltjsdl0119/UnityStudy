using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/NegativeAction", fileName = "Negative Action")]

public class NegativeAction : TaskAction
{
    public override int Run(Task task, int success, int s_count)
    {
        return s_count < 0 ? success - s_count : success;
    }
}