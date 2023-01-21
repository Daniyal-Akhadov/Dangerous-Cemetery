using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private ParticleSystem _deathFX;

        private bool _isDead;

        private void OnEnable() =>
            _heroHealth.Changed += OnHealthChanged;

        private void OnDisable() =>
            _heroHealth.Changed -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_isDead == false && _heroHealth.Current <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            _heroMovement.enabled = false;
            _heroAttack.enabled = false;
            _heroAnimator.PlayDeath();
            Instantiate(_deathFX, transform.position, Quaternion.identity);
            _isDead = true;
        }
    }
}