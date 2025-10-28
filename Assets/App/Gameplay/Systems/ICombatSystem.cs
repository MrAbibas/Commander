using System.Collections.Generic;
using App.Gameplay.Entities;
using App.Gameplay.Providers;
using UnityEngine.Events;

namespace App.Gameplay.Systems
{
    public interface ICombatSystem
    {
                
    }

    public class CombatSystem : ICombatSystem
    {
        public UnityEvent OnAllEnemyFormationsDestroyed;
        private List<Formation> _enemyFormations;
        private IFriendlyFormationSystem _friendlyFormationSystem;
        private IPlayerProvider _playerProvider;

        public bool IsFighting { get; private set; }

        public CombatSystem(List<Formation> enemyFormations,
            IFriendlyFormationSystem friendlyFormationSystem,
            IPlayerProvider playerProvider)
        {
            _enemyFormations = enemyFormations;
            _friendlyFormationSystem = friendlyFormationSystem;
            _playerProvider = playerProvider;
        }

        public void Update()
        {
            CheckAliveEnemyFormation();
        }

        private void CheckAliveEnemyFormation()
        {
            for (int i = 0; i < _enemyFormations.Count; i++)
            {
                var formation = _enemyFormations[i];
                if (formation.IsAlive == false)
                {
                    _enemyFormations.RemoveAt(i);
                    i--;
                }
            }

            if (_enemyFormations.Count == 0)
                OnAllEnemyFormationsDestroyed?.Invoke();
        }

        public void StartFight()
        {
            
        }
    }

    public class Formation
    {
        public List<Character> Characters { get; set; }
        public bool IsAlive { get; set; }
    }
}