using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;

        private const string Main = "Main";

        public LoadProgressState
        (
            GameStateMachine gameStateMachine,
            IPersistentProgressService persistentProgressService,
            ISaveLoadService saveLoadService
        )
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(_persistentProgressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _persistentProgressService.Progress =
                _saveLoadService.LoadProgress() ?? CreateProgress();

        private static PlayerProgress CreateProgress() =>
            new PlayerProgress(Main, 50f);
    }
}