using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//handles which state is active and when to transition to/from states
//creates a stack of states so if you remember a state, it knows what to fall back to
public class StateMachine : MonoBehaviour
{
    public Stack<State> States { get; set; } //auto implements the get/sets properties

    private void Awake()
    {
        States = new Stack<State>();
    }

    private void Update()
    {
        if (GetCurrentState() != null)
        {
            GetCurrentState().ActiveAction.Invoke();  //if we have a state let's invoke it every frame
        }
    }

    //push is going to take a state and put it on the top of the stack
    public void PushState(Action active, Action onEnter, Action onExit)
    {
        //whenever we move a state to the top, the previous state gets pushed down
        if (GetCurrentState() != null) //if there is a state
        {
            GetCurrentState().OnExit();
        }

        State state = new (active, onEnter, onExit);
        States.Push(state);

        GetCurrentState().OnEnter(); //set the state to its enter action

    }

    public void PushState(State newstate)
    {
        //whenever we move a state to the top, the previous state gets pushed down
        if (GetCurrentState() != null) //if there is a state
        {
            GetCurrentState().OnExit();
        }
        States.Push(newstate);
        GetCurrentState().OnEnter();
    }

    //pop removes a state from the top of the stack
    public void PopState()
    {
        if (GetCurrentState() != null)
        {
            GetCurrentState().OnExit();
            GetCurrentState().ActiveAction = null; //just gonna play it safe
            States.Pop();
            GetCurrentState().OnEnter();
        }
    }

    //get the current state
    private State GetCurrentState()
    {
        return States.Count > 0 ? States.Peek() : null;
        // Ternary operators rule, send tweet
    }
    public string GetState()
    {
        return GetCurrentState().name;
    }
    public void ResetStack()
    {
        States.Clear();
    }
}