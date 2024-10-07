using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Cant use "new" with MonoBehaviour and we don't want to inherit that stuff anyways
public class State
{
    public Action ActiveAction;
    public Action OnEnterAction; //what happens when we enter the state
    public Action OnExitAction; //what happens when we leave the state
    public string name;


    //let's create a constructor
    //state is going to be methods on our objects
    //while "in" a state a method will be executed every frame
    public State(Action active, Action onEnter, Action onExit, string stateName)
    {
        // We got 3 major things to keep in mind; what do we do when we start a state, when we are actively in the state, and when we leave a state
        // for example, when we exit
        // Ohio, we regain our sanity and possibly our will to live
        ActiveAction = active;
        OnEnterAction = onEnter;
        OnExitAction = onExit;
        name = stateName;
    }

    public State(Action active, Action onEnter, Action onExit)
    {
        ActiveAction = active;
        OnEnterAction = onEnter;
        OnExitAction = onExit;
        name = "unassigned";
    }

    //all of these execute the things our state machine can do
    //while something is active this runs
    public void Execute()
    {
        if (ActiveAction != null) //lets make sure there is an action we can take
        {
            ActiveAction.Invoke(); //call our action everyframe
        }
    }

    //whenever a state is added we'll call the OnEnter
    public void OnEnter()
    {
        if (OnEnterAction != null)
        {
            OnEnterAction.Invoke();
        }
    }

    //whenver a state is removed we'll call the OnExit
    public void OnExit()
    {
        if (OnExitAction != null)
        {
            OnExitAction.Invoke();
        }
    }


}
