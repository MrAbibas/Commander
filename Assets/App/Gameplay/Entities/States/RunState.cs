using UnityEngine;

namespace App.Gameplay.Entities.States
{
    public class RunState: BaseState
    {
        public RunState(Character character, Animator animator) : base(character, animator)
        {
        }

        public override void Enter()
        {
            animator.CrossFade(RunHash, crossFadeDuration);
        }

        public override void Update()
        {
            Character.HandleMovement();
        }
    }
}