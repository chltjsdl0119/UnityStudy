using System;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None,
    Idle,
    Move,
    Jump,
    Fall,
    Land
}

public class CharacterMachine : MonoBehaviour
{
    // Movement
    public virtual float horizontal { get; set; }
    public float speed;
    public Vector2 move;
    public bool isMovable;

    public State current;
    private Dictionary<State, IWorkflow<State>> _states;

    public void Initialize(IEnumerable<KeyValuePair<State, IWorkflow<State>>> copy)
    {
        _states = new Dictionary<State, IWorkflow<State>>(copy);
    }

    public bool ChangeState(State newState)
    {
        if (newState == current)
            return false;

        current = newState;
        return true;
    }

    private void Update()
    {
        ChangeState(_states[current].MoveNext());

        if (isMovable)
        {
            move = new Vector2(horizontal * speed, 0.0f);
        }
        
    }

    private void FixedUpdate()
    {
        // transform.position += move + Time.fixedDeltaTime;
    }
}