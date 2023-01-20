using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;

        private State _state;

        public float Current => _state.CurrentHealth;

        public float Max => _state.MaxHealth;

        public event Action Changed;

        public void TakeDamage(float damage)
        {
            if (Current > 0)
            {
                _state.CurrentHealth -= damage;
                _animator.PlayHit();
                Changed?.Invoke();
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            Changed?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState = _state;
        }
    }
}