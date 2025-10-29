using App.Core.FSM;
using UnityEngine;

namespace App.Gameplay.Entities.Characters.States
{
    public class BaseState: IState
    {
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int RunHash = Animator.StringToHash("Run");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");
        protected static readonly int DieHash = Animator.StringToHash("Die");
        protected const float crossFadeDuration = 0.1f;
        
        protected Character Character;
        protected Animator animator;

        protected BaseState(Character character, Animator animator)
        {
            this.Character = character;
            this.animator = animator;
        }
        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }
    }
}