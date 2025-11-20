using System;
using App.Core.FSM;
using App.Gameplay.Entities.Characters.States;
using App.Gameplay.Systems;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace App.Gameplay.Entities.Characters
{
    public class AICharacter : Character
    {
        [field: SerializeField] public NavMeshAgent Agent { get; protected set; }
        public Vector3 TargetPosition { get; protected set; }
        public Transform PlaceInFormation { get; set; }

        [Inject]
        public void Construct()
        {
            
        }
        


        public override bool IsMoving() => Mathf.Approximately(Agent.velocity.sqrMagnitude, 0) == false;
        public void SetTargetPosition(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
            Agent.SetDestination(targetPosition);
        }
    }
}