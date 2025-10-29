using App.Core.FSM;
using UnityEngine;

namespace App.Gameplay.Entities.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [field: SerializeField] public EntityId EntityId { get; private set; }
        [SerializeField] protected float speed;
        [SerializeField] protected CharacterController characterController;
        [SerializeField] protected Animator animator;
        protected StateMachine StateMachine;
        protected Character Target;
        public bool IsAlive { get; set; }

        public virtual void HandleMovement(){}

        public abstract bool IsMoving();
        public bool HasTarget() => Target != null;
        public void SetTarget(Character target) => Target = target;
    }

    public enum EntityId
    {
        Player,
        
    }
}