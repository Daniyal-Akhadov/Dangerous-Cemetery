using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Hero
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;

        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HeroMovement _))
            {
                _saveLoadService.SaveProgress();
                Debug.Log("Progress is saved.");
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (_boxCollider != null)
            {
                Gizmos.color = new Color32(43, 211, 78, 120);
                Gizmos.DrawCube(transform.position + _boxCollider.center, _boxCollider.size);
            }
        }
    }
}