using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _front;

        public void SetValue(float current, float max) =>
            _front.fillAmount = current / max;
    }
}