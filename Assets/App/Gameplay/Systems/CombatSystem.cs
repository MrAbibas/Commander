using System.Collections.Generic;
using App.Gameplay.Configurations;
using App.Gameplay.Entities;
using App.Gameplay.Entities.Characters;
using App.Gameplay.Providers;
using App.Services;
using UnityEngine;
using UnityEngine.Events;

namespace App.Gameplay.Systems
{
    public class CombatSystem : ICombatSystem
    {
        public UnityEvent OnAllEnemyFormationsDestroyed;
        private List<Formation> _enemyFormations;
        private Formation _friendlyFormation;
        private IPlayerProvider _playerProvider;
        private CombatConfiguration _combatConfiguration;

        public bool IsFighting { get; private set; }

        public CombatSystem(List<Formation> enemyFormations,
            Formation friendlyFormation,
            IPlayerProvider playerProvider,
            IConfigurationService configurationService)
        {
            _enemyFormations = enemyFormations;
            _friendlyFormation = friendlyFormation;
            _playerProvider = playerProvider;
            _combatConfiguration = configurationService.GetConfiguration<CombatConfiguration>();
        }

        public void Update()
        {
            CheckAliveEnemyFormation();
            CheckFriendlyFormation();

            var player = _playerProvider.Player;
            Vector3 playerPosition = player.transform.position;
            float playerToEnemyMinDistance = float.MaxValue;
            Vector3 enemyPos;
            float distance = 0;
            float enemyRange = 0;
            float playerRange = _combatConfiguration.Ranges[EntityID.Player];
            foreach (var formation in _enemyFormations)
            {
                foreach (var enemy in formation.Characters)
                {
                    enemyPos = enemy.transform.position;
                    enemyRange = _combatConfiguration.Ranges[enemy.EntityId];
                    distance = Vector3.SqrMagnitude(playerPosition - enemyPos);
                    if (distance < playerRange && distance < playerToEnemyMinDistance)
                    {
                        playerToEnemyMinDistance = distance;
                        player.SetTarget(enemy);
                    }
                    if (distance <= enemyRange)
                    {
                        enemy.SetTarget(player);
                        continue;
                    }

                    if (IsFighting)
                    {
                        foreach (var friendly in _friendlyFormation.Characters)
                        {
                            distance = Vector3.SqrMagnitude(friendly.transform.position - enemyPos);
                            if(distance < _combatConfiguration.Ranges[friendly.EntityId])
                                friendly.SetTarget(enemy);
                            
                            if(distance < enemyRange)
                                enemy.SetTarget(friendly);
                        }
                    }
                }
            }
        }

        private void CheckFriendlyFormation()
        {
            if (_friendlyFormation.CheckIsAlive() == false)
                IsFighting = false;
        }

        private void CheckAliveEnemyFormation()
        {
            for (int i = 0; i < _enemyFormations.Count; i++)
            {
                var formation = _enemyFormations[i];
                if (formation.CheckIsAlive() == false)
                {
                    _enemyFormations.RemoveAt(i);
                    i--;
                }
            }

            if (_enemyFormations.Count == 0)
            {
                IsFighting = false;
                OnAllEnemyFormationsDestroyed?.Invoke();
            }
        }

        public void StartFight()
        {
            if(_enemyFormations.Count == 0) return;
            IsFighting = true;
            _friendlyFormation.SetTargetPosition(_enemyFormations[0].transform.position);
        }
    }
}