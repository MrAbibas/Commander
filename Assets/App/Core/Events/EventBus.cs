using System;
using System.Collections.Generic;

namespace App.Core.Events
{
    public class EventBus<TEvent>: IDisposable where TEvent: IGameEvent
    {
        private readonly Dictionary<Type, Delegate> _eventHandlers = new Dictionary<Type, Delegate>();

        public void Subscribe<T>(Action<T> handler) where T : TEvent
        {
            if (_eventHandlers.TryGetValue(typeof(T), out var existingHandlers))
            {
                _eventHandlers[typeof(T)] = Delegate.Combine(existingHandlers, handler);
            }
            else
            {
                _eventHandlers[typeof(T)] = handler;
            }
        }

        public void Unsubscribe<T>(Action<T> handler) where T : TEvent
        {
            if (_eventHandlers.TryGetValue(typeof(T), out var existingHandlers))
            {
                var newHandlers = Delegate.Remove(existingHandlers, handler);
                if (newHandlers == null)
                    _eventHandlers.Remove(typeof(T));
                else
                    _eventHandlers[typeof(T)] = newHandlers;
            }
        }

        public void Publish<T>(T gameEvent) where T : TEvent
        {
            if (_eventHandlers.TryGetValue(typeof(T), out var handlers))
            {
                ((Action<T>)handlers)?.Invoke(gameEvent);
            }
        }

        public void Dispose()
        {
            _eventHandlers.Clear();
        }
    }
}
