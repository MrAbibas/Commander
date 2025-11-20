using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.Entities.Characters.States
{
    public class FriendlyAIIdleState : BaseState
    {
        private AICharacter _aiCharacter;
        
        public FriendlyAIIdleState(Character character, Animator animator) : base(character, animator)
        {
            _aiCharacter = character as AICharacter;
        }

        public override void Enter()
        {
            animator.CrossFade(IdleHash, crossFadeDuration);
            _aiCharacter.Agent.updateRotation = false;
            if (_aiCharacter.PlaceInFormation != null)
            {
                float angle = Vector3.Angle(_aiCharacter.transform.forward, _aiCharacter.PlaceInFormation.forward);
                _aiCharacter.transform.DORotateQuaternion(_aiCharacter.PlaceInFormation.rotation,
                    angle / _aiCharacter.Agent.angularSpeed);
            }
        }

        public override void Exit()
        {
            _aiCharacter.Agent.updateRotation = true;
        }
        
    }
}