using System.Linq;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Utilities;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _overlapRadius = 1f;
        [SerializeField] private float _effectiveDistance = 0.5f;
        [SerializeField] private float _damage = 5f;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _timer;
        private bool _isAttack;
        private int _targetLayer;
        private bool _isAttackActive;

        private readonly Collider[] _hits = new Collider[1];

        private bool CanAttack => _isAttackActive == true && _timer <= 0f && _isAttack == false;

        private Vector3 StartPoint =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * _effectiveDistance;

        private void Awake()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
            _targetLayer = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (CanAttack == true)
                StartAttack();
        }

        public void Enable()
        {
            _isAttackActive = true;
        }

        public void Disable()
        {
            _isAttackActive = false;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint, _overlapRadius, 2f);
                hit.transform.GetComponent<HeroHealth>().TakeDamage(_damage);
            }
        }

        private bool Hit(out Collider hit)
        {
            int size = Physics.OverlapSphereNonAlloc
            (
                StartPoint, _overlapRadius, _hits, _targetLayer
            );

            hit = _hits.FirstOrDefault();
            return size > 0;
        }

        private void OnAttackEnded()
        {
            _timer = _attackCooldown;
            _isAttack = false;
        }

        private void StartAttack()
        {
            _isAttack = true;
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();
        }

        private void OnHeroCreated()
        {
            _heroTransform = _gameFactory.Hero.transform;
        }
    }
}