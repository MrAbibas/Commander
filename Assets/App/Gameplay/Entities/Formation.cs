using System.Collections.Generic;
using App.Gameplay.Entities.Characters;
using UnityEngine;

namespace App.Gameplay.Entities
{
    public class Formation : MonoBehaviour
    {
        [SerializeField]
        protected List<Transform> points;
        [field: SerializeField]
        public List<AICharacter> Characters { get; private set; } = new();

        public bool IsFull => Characters.Count >= points.Count;

        public bool CheckIsAlive()
        {
            for (int i = 0; i < points.Count; i++)
            {
                var character = Characters[i];
                if (character.IsAlive == false)
                    RemoveCharacter(character);
            }

            return Characters.Count != 0;
        }

        public void AddCharacter(AICharacter character)
        {
            if (Characters.Count >= points.Count) return;
            Characters.Add(character);
            character.PlaceInFormation = points[Characters.Count - 1].transform;
            character.SetTargetPosition(points[Characters.Count - 1].position);
        }

        public void RemoveCharacter(AICharacter character)
        {
            Characters.Remove(character);
            character.PlaceInFormation = null;
            UpdateCharactersPositions();
        }

        public virtual void SetTargetPosition(Vector3 position)
        {
            transform.position = position;
            UpdateCharactersPositions();
        }

        private void UpdateCharactersPositions()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].PlaceInFormation = points[i].transform;
                Characters[i].SetTargetPosition(points[i].position);
            }
        }
    }
}