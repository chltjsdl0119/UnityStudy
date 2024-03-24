using System;
using UnityEngine;

public class PlayerMachine : CharacterMachine
{
    public override float horizontal
    {
        get => Input.GetAxisRaw("Horizontal");
        set => base.horizontal = value;
    }

    private void Start()
    {
        Initialize(CharacterStateWorkflowsDataSheet.GetWorkflowsForPlayer(this));
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.Space))
        {
            ChangeState(State.Jump);
        }
    }
}