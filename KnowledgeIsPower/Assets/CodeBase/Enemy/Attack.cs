using System.Linq;
using CodeBase.Logic;
using CodeBase.Utilities;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;

        private Transform _hero;
        private float _timer;
        private bool _isAttack;
        private int _targetLayer;
        private bool _isAttackActive;

        private readonly Collider[] _hits = new Collider[1];

        private bool CanAttack => _isAttackActive == true && _timer <= 0f && _isAttack == false;

        private Vector3 StartPoint =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;

        public float AttackCooldown { get; set; }

        public float AttackRadius { get; set; }

        public float EffectiveDistance { get; set; }

        public float Damage { get; set; }

        private void Awake()
        {
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

        public void Construct(Transform hero)
        {
            _hero = hero;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                print("Hit ");
                PhysicsDebug.DrawDebug(StartPoint, AttackRadius, 2f);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _timer = AttackCooldown;
            _isAttack = false;
        }

        private bool Hit(out Collider hit)
        {
            int size = Physics.OverlapSphereNonAlloc
            (
                StartPoint, AttackRadius, _hits, _targetLayer
            );

            hit = _hits.FirstOrDefault();
            return size > 0;
        }

        private void StartAttack()
        {
            _isAttack = true;
            transform.LookAt(_hero);
            _animator.PlayAttack();
        }
    }
}