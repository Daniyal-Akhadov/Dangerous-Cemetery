using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private HeroAnimator _animator;

        private State _state;

        public float Current
        {
            get => _state.CurrentHealth;
            set => _state.CurrentHealth = value;
        }

        public float Max
        {
            get => _state.MaxHealth;
            set => _state.MaxHealth = value;
        }

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