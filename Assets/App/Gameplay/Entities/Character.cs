using App.Core.FSM;
using UnityEngine;

namespace App.Gameplay.Entities
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected CharacterController characterController;
        [SerializeField] protected Animator animator;
        protected StateMachine StateMachine;
        protected Character Target;
        
        public virtual void HandleMovement(){}

        public abstract bool IsMoving();
        public bool HasTarget() => Target != null;
    }

    public enum EntityId
    {
        Player,
        
    }
}