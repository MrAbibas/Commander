using UnityEngine;

namespace App.Gameplay.Entities.Characters.States
{
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
}