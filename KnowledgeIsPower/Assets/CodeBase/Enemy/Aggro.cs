using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMovementToPlayer _follow;
        [SerializeField] private float _cooldown = 1f;

        private Coroutine _aggroCoroutine;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider _)
        {
            if (_aggroCoroutine != null)
            {
                StopAggroCoroutine();
            }

            _follow.Enable();
        }

        private void TriggerExit(Collider _)
        {
            _aggroCoroutine = StartCoroutine(MoveCooldownTime());
        }

        private IEnumerator MoveCooldownTime()
        {
            yield return new WaitForSeconds(_cooldown);
            _follow.Disable();
            _aggroCoroutine = null;
        }

        private void StopAggroCoroutine()
        {
            StopCoroutine(_aggroCoroutine);
            _aggroCoroutine = null;
        }
    }
}