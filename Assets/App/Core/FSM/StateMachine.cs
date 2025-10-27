using System;
using System.Collections.Generic;

namespace Core.FSM
{
    public class StateMachine
    {
        private StateNode current;
        private Dictionary<Type, StateNode> nodes = new();
        private HashSet<ITransition> anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if(transition != null)
                ChangeState(transition.To);
            
            current.State?.Update();
        }

        public void SetState(IState state)
        {
            current = nodes[state.GetType()];
            current.State?.Enter();
        }

        public void ChangeState(IState state)
        {
            if (state == current.State) return;
            
            current.State?.Exit();
            current = nodes[state.GetType()];
            current.State?.Enter();
        }

        public ITransition GetTransition()
        {
            foreach (var transition in anyTransitions)
                if(transition.Condition.Evaluate())
                    return transition;
            
            foreach(var transition in current.Transitions)
                if(transition.Condition.Evaluate())
                    return transition;
            
            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(to, condition));
        }

        public StateNode GetOrAddNode(IState state)
        {
            if(nodes.TryGetValue(state.GetType(), out var node))
                return node;
            
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
            return node;
        }
    }
}