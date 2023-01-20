using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState
        (
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            Curtain curtain,
            IGameFactory gameFactory,
            IPersistentProgressService progressService
        )
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadLevel(sceneName, OnLoaded);
            _curtain.Show();
            _gameFactory.Cleanup();
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitialGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitialGameWorld()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            SetupCameraFollower(hero);
            _gameFactory.CreateHud();
        }

        private static void SetupCameraFollower(GameObject target) =>
            Camera.main.GetComponent<CameraFollower>().SetTarget(target.transform);
    }
}