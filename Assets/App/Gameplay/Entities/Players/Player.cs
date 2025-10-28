using System;
using App.Core.FSM;
using App.Gameplay.Entities.States;
using App.Gameplay.Systems;
using UnityEngine;

namespace App.Gameplay.Entities.Players
{
    public class Player : Character
    {
        private IPlayerInputSystem _input;
        private Vector3 _moveDirection;

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

        private void Update()
        {
            
            StateMachine.Update();
        }
        
        public override bool IsMoving() => _input.MoveDirection != Vector2.zero;

        public override void HandleMovement()
        {
            Vector2 input = _input.MoveDirection;
            
            _moveDirection = new Vector3(input.x, 0f, input.y);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= speed;
            characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
}
