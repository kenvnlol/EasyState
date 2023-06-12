using EasyState.Registrar;
using Microsoft.Extensions.DependencyInjection;

namespace EasyState;

public class StateMachine<TState, TEvent, TClass>
    where TState : notnull
    where TEvent : notnull
    where TClass : class
{
    public Dictionary<TState, ICollection<StateTransition<TState, TEvent, TClass>>> _legalTransitions;

    public StateMachine(Dictionary<TState, ICollection<StateTransition<TState, TEvent, TClass>>> legalTransitions)
    {
        _legalTransitions = legalTransitions;
    }

    public TState Transition(TState currentState, TEvent ev)
    {
        if (!_legalTransitions.TryGetValue(currentState, out var legalTransitions))
        {
            throw new ArgumentException($"Current state is not valid for transitions.");
        }

        var transitionableState = legalTransitions.FirstOrDefault(x => x.Event.Equals(ev));

        if (transitionableState == null)
        {
            throw new ArgumentException("Event is not valid for current state.");
        }

        var guard = ServiceLocator.ServiceProvider.GetService<TClass>();
        if (guard == null)
        {
            throw new InvalidOperationException($"The service {typeof(TClass).FullName} could not be resolved.");
        }


        if (transitionableState.Guard != null && !transitionableState.Guard.Invoke(guard))
        {
            throw new InvalidOperationException("Transition validation failed.");
        }

        return transitionableState.NextState;
    }
}
