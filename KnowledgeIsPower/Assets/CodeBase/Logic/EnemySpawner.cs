using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(UniqueId))]
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterType;

        private string _id;
        private bool _isSlain;
        private IGameFactory _factory;
        private EnemyDeath _monster;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = ServiceLocator.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id) == true)
            {
                _isSlain = true;
            }
            else
            {
                Spawn();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isSlain == true)
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(_monsterType, transform);
            _monster = monster.GetComponent<EnemyDeath>();
            _monster.Happened += OnMonsterDeath;
        }

        private void OnMonsterDeath()
        {
            if (_monster != null)
                _monster.Happened -= OnMonsterDeath;
            
            _isSlain = true;
        }
    }

    public enum MonsterTypeId
    {
        Lich = 0,
        Golem = 10
    }
}