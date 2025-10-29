using App.Core.FSM;
using App.Gameplay.Entities.Characters.States;
using App.Gameplay.Systems;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Entities.Characters.Players
{
    public class Player : Character
    {
        private IPlayerInputSystem _input;
        private Vector3 _moveDirection;

        [Inject]
        public void Construct(IPlayerInputSystem input)
        {
            _input = input;
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

        private void Update()
        {
            
            StateMachine.Update();
        }
        
        public override bool IsMoving() => _input.MoveDirection != Vector2.zero;

        public override void HandleMovement()
        {
            Vector2 input = _input.MoveDirection;
            
            // _moveDirection = new Vector3(input.x, 0f, input.y);
            // _moveDirection = transform.TransformDirection(_moveDirection);
            // _moveDirection *= speed;
            Vector3 lookDirection = new Vector3(input.x, 0f, input.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = targetRotation;
            characterController.Move(transform.forward * (speed * Time.deltaTime));
        }
    }
}
