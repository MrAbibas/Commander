using App.Core.FSM;
using App.Gameplay.Entities.Characters.States;

namespace App.Gameplay.Entities.Characters
{
    public class FriendlyAICharacter : AICharacter
    {
        private void Awake()
        {
            StateMachine = new StateMachine();
            var idleState = new FriendlyAIIdleState(this, animator);
            var runState = new RunState(this, animator);
            var attackState = new AttackState(this, animator);
            
            StateMachine.AddTransition(idleState, runState, new FuncPredicate(IsMoving));
            StateMachine.AddTransition(idleState, attackState, new FuncPredicate(() => IsMoving() == false && HasTarget()));
            
            StateMachine.AddTransition(runState, idleState,new FuncPredicate(() => IsMoving() == false));
            
            StateMachine.AddTransition(attackState, idleState,new FuncPredicate(() => IsMoving() == false && HasTarget() == false));
            StateMachine.AddTransition(attackState, runState, new FuncPredicate(IsMoving));
            StateMachine.SetState(idleState);
        }
    }
}