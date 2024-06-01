using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameLogic
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private void OnValidate() => 
            _image ??= GetComponent<Image>();

        public void UpdateProgress(float current, float max) => 
            _image.fillAmount = current / max;
    }
}
