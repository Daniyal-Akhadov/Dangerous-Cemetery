using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(AgentMovementToPlayer))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        private const float DestroyTimeAfterDeath = 3f;

        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private AgentMovementToPlayer _agent;
        [SerializeField] private ParticleSystem _deathFx;

        private bool _isDead;

        public event Action Happened;

        private void OnEnable()
        {
            _enemyHealth.Changed += OnHealthChanged;
        }

        private void OnDisable()
        {
            _enemyHealth.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_isDead == false && _enemyHealth.Current <= 0f)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _agent.enabled = false;
            _enemyAnimator.PlayDeath();
            CreateEffect();
            StartCoroutine(DestroyInTime());

            Happened?.Invoke();
        }

        private IEnumerator DestroyInTime()
        {
            yield return new WaitForSeconds(DestroyTimeAfterDeath);
            Destroy(gameObject);
        }

        private void CreateEffect()
        {
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}