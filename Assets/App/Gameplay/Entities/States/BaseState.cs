using App.Core.FSM;
using UnityEngine;

namespace App.Gameplay.Entities.States
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

    public class IdleState : BaseState
    {
        public IdleState(Character character, Animator animator) : base(character, animator)
        {
        }

        public override void Enter()
        {
            animator.CrossFade(IdleHash, crossFadeDuration);
        }
    }
    public class RunState: BaseState
    {
        public RunState(Character character, Animator animator) : base(character, animator)
        {
        }

        public override void Enter()
        {
            animator.CrossFade(RunHash, crossFadeDuration);
        }
    }
    public class AttackState: BaseState
    {
        public AttackState(Character character, Animator animator) : base(character, animator)
        {
        }
    }
}