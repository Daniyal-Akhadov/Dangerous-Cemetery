using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject Hero;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            Hero = InstantiateRegistered(AssetsPath.HeroPath, initialPoint.transform.position);
            return Hero;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(prefabPath: AssetsPath.HudPath);
            hud.GetComponentInChildren<ActorUI>().Construct(Hero.GetComponentInChildren<HeroHealth>());
            return hud;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticDataService.GetMonsterById(typeId);
            GameObject monsterInstance = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);
            IHealth health = monsterInstance.GetComponent<IHealth>();
            health.Max = health.Max;
            health.Current = health.Max;

            monsterInstance.GetComponent<ActorUI>().Construct(health);
            monsterInstance.GetComponent<AgentMovementToPlayer>().Construct(Hero.transform);
            monsterInstance.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            Attack attack = monsterInstance.GetComponent<Attack>();
            attack.Construct(Hero.transform);
            attack.Damage = monsterData.Damage;
            attack.AttackCooldown = monsterData.Cooldown;
            attack.EffectiveDistance = monsterData.EffectiveDistance;
            attack.AttackRadius = monsterData.RadiusAttack;
            return monsterInstance;
        }

        public void Cleanup()
        {
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject instance = _assetProvider.Instantiate(path: prefabPath, at);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject instance = _assetProvider.Instantiate(path: prefabPath);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private void RegisterProgressWatchers(GameObject instance)
        {
            foreach (ISavedProgressReader progressReader in instance.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}