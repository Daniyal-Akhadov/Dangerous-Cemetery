using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner, Curtain curtain)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, ServiceLocator.Container);
        }
    }
}