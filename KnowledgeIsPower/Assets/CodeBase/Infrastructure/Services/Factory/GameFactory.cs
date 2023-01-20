using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject Hero { get; private set; }
        public event Action HeroCreated;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            Hero = InstantiateRegistered(AssetsPath.HeroPath, initialPoint.transform.position);
            HeroCreated?.Invoke();
            return Hero;
        }

        public GameObject CreateHud() =>
            InstantiateRegistered(prefabPath: AssetsPath.HudPath);

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

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}