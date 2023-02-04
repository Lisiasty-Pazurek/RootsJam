using Assets.Script.GRLO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState _currentState;

    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> FromCurrentTransitions = new List<Transition>();
    private List<Transition> FromAnyTransitions = new List<Transition>();

    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {

        Transition transition = null;
        try
        {

            transition = GetTransition();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        try
        {
            if (transition != null)
                SetState(transition.To);

        }
        catch (Exception e)
        {
            Debug.Log(e);

        }

        try
        {
            _currentState?.Tick();

        }
        catch (Exception e)
        {
            Debug.Log(e);

        }
    }

    public void SetState(IState state)
    {
        if (state == _currentState)
            return;

        _currentState?.OnExit();
        _currentState = state;

        _transitions.TryGetValue(_currentState.GetType(), out FromCurrentTransitions);
        if (FromCurrentTransitions == null)
            FromCurrentTransitions = EmptyTransitions;

        _currentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));
    }

    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        FromAnyTransitions.Add(new Transition(state, predicate));
    }

    private class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in FromAnyTransitions)
            if (transition.Condition())
                return transition;

        foreach (var transition in FromCurrentTransitions)
            if (transition.Condition())
                return transition;

        return null;
    }
}