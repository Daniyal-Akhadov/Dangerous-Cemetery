using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Services.Factory;
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

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadLevel(sceneName, OnLoaded);
            _curtain.Show();
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject hero = _gameFactory.CreateHero(initialPoint);
            _gameFactory.CreateHud();
            SetupCameraFollower(hero);
            _gameStateMachine.Enter<GameLoopState>();
        }

        private static void SetupCameraFollower(GameObject target) =>
            Camera.main.GetComponent<CameraFollower>().SetTarget(target.transform);
    }
}