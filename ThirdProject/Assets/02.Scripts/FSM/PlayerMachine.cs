using System;

public class PlayerMachine : CharacterMachine
{
    private void Start()
    {
        Initialize(CharacterStateWorkflowsDataSheet.GetWorkflowsForPlayer(this));
    }
}