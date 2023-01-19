using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Curtain : MonoBehaviour
    {
        [Range(0.01f, 0.05f)] [SerializeField] private float _fadeInStep = 0.03f;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1f;
        }

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_fadeInStep);

            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= _fadeInStep;
                yield return waitForSeconds;
            }

            gameObject.SetActive(false);
        }
    }
}