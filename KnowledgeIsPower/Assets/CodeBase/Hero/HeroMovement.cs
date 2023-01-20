using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float _speed = 5f;

        private CharacterController _characterController;
        private Camera _camera;
        private IInputService _inputService;

        private static string CurrentLevel => SceneManager.GetActiveScene().name;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 targetMovePoint = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                targetMovePoint = _camera.transform.TransformDirection(_inputService.Axis);
                targetMovePoint.y = 0f;
                targetMovePoint.Normalize();
                transform.forward = targetMovePoint;
            }

            targetMovePoint += Physics.gravity;
            _characterController.Move(targetMovePoint * (_speed * Time.deltaTime));
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel, transform.position.AsVectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                {
                    Warp(to: savedPosition);
                }
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }
    }
}