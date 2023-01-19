using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _angleX = 45f;
        [SerializeField] private float _distance = 2f;
        [SerializeField] private float _offsetY;

        private Transform _target;

        private Vector3 TargetPointPosition
        {
            get
            {
                Vector3 targetPosition = _target.position;
                targetPosition.y += _offsetY;
                return targetPosition;
            }
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Quaternion rotation = Quaternion.Euler(Vector3.right * _angleX);
            Vector3 position = rotation * (Vector3.forward * -_distance) + TargetPointPosition;

            transform.position = position;
            transform.rotation = rotation;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}