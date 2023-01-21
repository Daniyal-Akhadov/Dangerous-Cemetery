using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.Changed += OnHealthChanged;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDisable()
        {
            if (_health != null)
                _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _healthBar.SetValue(_health.Current, _health.Max);
        }
    }
}