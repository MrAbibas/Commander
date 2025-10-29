using UnityEngine;
using UnityEngine.AI;

namespace App.Gameplay.Entities.Characters
{
    public class AICharacter : Character
    {
        [SerializeField] private NavMeshAgent agent;
        public Vector3 TargetPosition { get; private set; }

        public override bool IsMoving() => Mathf.Approximately(agent.velocity.sqrMagnitude, 0);
        public void SetTargetPosition(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
            agent.SetDestination(targetPosition);
        }
    }
}