using CodeBase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(GameObject initialPoint) =>
            _assetProvider.Instantiate(path: AssetsPath.HeroPath, at: initialPoint.transform.position);

        public GameObject CreateHud() =>
            _assetProvider.Instantiate(path: AssetsPath.HudPath);
    }
}