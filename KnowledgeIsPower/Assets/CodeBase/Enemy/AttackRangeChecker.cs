using CodeBase.Utilities;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class AttackRangeChecker : MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

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
            _attack.Enable();
        }

        private void TriggerExit(Collider _)
        {
            _attack.Disable();
        }
    }
}