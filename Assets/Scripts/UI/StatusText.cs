using TMPro;
using UnityEngine;

namespace UI
{
    public class StatusText : MonoBehaviour
    {
        [SerializeField] private TMP_Text textMeshPro;
        private new Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.LookAt(camera.transform);
        }

        public void UpdateText(string text)
        {
            textMeshPro.text = $"Stamina: {text}";
        }
    }
}