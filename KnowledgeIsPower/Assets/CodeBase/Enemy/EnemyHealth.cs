using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;

        public float Current { get; set; }

        public float Max { get; set; }

        public event Action Changed;

        public void TakeDamage(float damage)
        {
            Current -= damage;
            _enemyAnimator.PlayHit();
            Changed?.Invoke();
        }
    }
}