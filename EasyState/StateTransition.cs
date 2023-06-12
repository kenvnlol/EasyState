using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyState;

public class StateTransition<TState, TEvent, TClass>
{
    public required TEvent Event { get; set; }
    public required TState NextState { get; set; }
    public Func<TClass, bool>? Guard { get; set; }
}

