using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private const string HittableLayer = "Hittable";

        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private Abilities _abilities;
        private int _layer;
        private bool _isAttack;

        private readonly Collider[] _hits = new Collider[3];

        private Vector3 StartPoint =>
            new Vector3(transform.position.x, _characterController.height / 2f, transform.position.z);

        private void Awake()
        {
            _layer = 1 << LayerMask.NameToLayer(HittableLayer);
        }

        private void Start()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonDown() && _heroAnimator.IsAttacking == false)
            {
                _heroAnimator.PlayAttack();
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _abilities = progress.Abilities;
        }

        private void OnAttack()
        {
            if (Hit() > 0)
            {
                for (int i = 0; i < Hit(); i++)
                {
                    _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_abilities.Damage);
                }
            }
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint + transform.forward, _abilities.AttackRadius, _hits, _layer);
    }
}