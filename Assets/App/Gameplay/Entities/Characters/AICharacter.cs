using System;
using App.Core.FSM;
using App.Gameplay.Entities.Characters.States;
using App.Gameplay.Systems;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace App.Gameplay.Entities.Characters
{
    public class AICharacter : Character
    {
        [SerializeField] private NavMeshAgent agent;
        public Vector3 TargetPosition { get; private set; }

        [Inject]
        public void Construct()
        {
            
        }
        
        private void Awake()
        {
            StateMachine = new StateMachine();
            var idleState = new IdleState(this, animator);
            var runState = new RunState(this, animator);
            var attackState = new AttackState(this, animator);
            
            StateMachine.AddTransition(idleState, runState, new FuncPredicate(IsMoving));
            StateMachine.AddTransition(idleState, attackState, new FuncPredicate(() => IsMoving() == false && HasTarget()));
            
            StateMachine.AddTransition(runState, idleState,new FuncPredicate(() => IsMoving() == false));
            
            StateMachine.AddTransition(attackState, idleState,new FuncPredicate(() => IsMoving() == false && HasTarget() == false));
            StateMachine.AddTransition(attackState, runState, new FuncPredicate(IsMoving));
            StateMachine.SetState(idleState);
        }

        public override bool IsMoving() => Mathf.Approximately(agent.velocity.sqrMagnitude, 0) == false;
        public void SetTargetPosition(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
            agent.SetDestination(targetPosition);
        }
        
    }
}