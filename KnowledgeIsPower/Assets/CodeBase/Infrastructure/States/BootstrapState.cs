using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private static ServiceLocator _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = serviceLocator;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.LoadLevel(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private static void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>
            (
                new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>())
            );
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>
            (
                new SaveLoadService
                (
                    _services.Single<IGameFactory>(),
                    _services.Single<IPersistentProgressService>()
                )
            );
        }

        private static void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadMonsters();
            _services.RegisterSingle<IStaticDataService>(staticDataService);
        }

        private static IInputService GetInputService()
        {
            IInputService result = null;

            if (Application.isMobilePlatform)
                result = new MobileInputService();
            else if (Application.isEditor)
                result = new PCInputService();

            return result;
        }
    }
}