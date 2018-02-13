using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Daenet.Common.Logging.EventHub
{
    /// <summary>
    /// Handles scopes.
    /// </summary>
    internal class EventHubLogScopeManager
    {
        private readonly string _name;
        private readonly object _state;

        internal EventHubLogScopeManager(string name, object state)
        {
            _name = name;
            _state = state;
        }

        public EventHubLogScopeManager Parent { get; private set; }

        private static AsyncLocal<EventHubLogScopeManager> _value = new AsyncLocal<EventHubLogScopeManager>();
        public static EventHubLogScopeManager Current
        {
            set
            {
                _value.Value = value;
            }
            get
            {
                return _value.Value;
            }
        }

        public static IDisposable Push(string name, object state)
        {
            var temp = Current;
            Current = new EventHubLogScopeManager(name, state);
            Current.Parent = temp;

            return new DisposableScope();
        }

        public override string ToString()
        {
            return _state?.ToString();
        }

        private class DisposableScope : IDisposable
        {
            public void Dispose()
            {
                Current = Current.Parent;
            }
        }
    }
}
