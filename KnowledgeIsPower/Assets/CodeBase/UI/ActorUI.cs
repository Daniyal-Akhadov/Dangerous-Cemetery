using System;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private HeroHealth _heroHealth;

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.Changed += OnHealthChanged;
        }

        private void OnDisable()
        {
            _heroHealth.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _healthBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}