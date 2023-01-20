using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMovementToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _hero;
        private IGameFactory _gameFactory;
        private bool _isMovementEnabled;

        private bool IsNotReached =>
            Vector3.Distance(transform.position, _hero.position) >= _agent.stoppingDistance;

        private bool CanMove =>
            _hero != null && _isMovementEnabled == true && IsNotReached == true;

        private void Awake()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        private void OnEnable()
        {
            _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Start()
        {
            if (_gameFactory.Hero != null)
                InitializeHero();
        }

        private void Update()
        {
            if (CanMove)
                _agent.destination = _hero.transform.position;
        }

        private void OnDisable()
        {
            _gameFactory.HeroCreated -= OnHeroCreated;
        }

        public void Enable()
        {
            _isMovementEnabled = true;
        }

        public void Disable()
        {
            _isMovementEnabled = false;
        }

        private void OnHeroCreated() =>
            InitializeHero();

        private void InitializeHero() =>
            _hero = _gameFactory.Hero.transform;
    }
}