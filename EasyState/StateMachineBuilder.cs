using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyState;

public class StateMachineBuilder<TState, TEvent, TClass>
    where TState : notnull
    where TEvent : notnull
    where TClass : class
{
    public TState? CurrentState { get; set; }
    public string? CurrentId { get; set; }
    public Dictionary<TState, ICollection<StateTransition<TState, TEvent, TClass>>> LegalTransitions { get; set; } = new Dictionary<TState, ICollection<StateTransition<TState, TEvent, TClass>>>();

    public StateMachineBuilder<TState, TEvent, TClass> ForState(TState currentState)
    {
        CurrentState = currentState;
        return this;
    }
    public StateMachineBuilder<TState, TEvent, TClass> CanTransitionTo(TState nextState, TEvent ev, Func<TClass, bool>? guard = null)
    {
        if (CurrentState == null)
        {
            throw new InvalidOperationException("Please call ForState method before CanTransitionTo");
        }

        if (!LegalTransitions.TryGetValue(CurrentState, out var existingState))
        {
            LegalTransitions[CurrentState] = new List<StateTransition<TState, TEvent, TClass>>();
        };


        LegalTransitions[CurrentState].Add(new StateTransition<TState, TEvent, TClass>
        {
            Event = ev,
            NextState = nextState,
            Guard = guard
        });


        return this;
    }

    public StateMachine<TState, TEvent, TClass> Build()
        => new StateMachine<TState, TEvent, TClass>(LegalTransitions);
}

