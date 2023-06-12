using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyState;

public class StateMachineConfiguration<TState, TEvent, TClass>
    where TState : notnull
    where TEvent : notnull
    where TClass : notnull
{
    public required string Id { get; set; }
    public Dictionary<TState, ICollection<StateTransition<TState, TEvent, TClass>>> LegalTransitions { get; set; } = new Dictionary<TState, ICollection<StateTransition<TState, TEvent, TClass>>>();
}
