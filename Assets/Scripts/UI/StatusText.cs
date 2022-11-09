using TMPro;
using UnityEngine;

namespace UI
{
    public class StatusText : MonoBehaviour
    {
        [SerializeField] private TMP_Text textMeshPro;
        [SerializeField] private PlayableCharacter character;
        private new Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.LookAt(camera.transform);
            textMeshPro.text = character.Hp.ToString();
        }

        public void UpdateStaminaUI(string text)
        {
            textMeshPro.text = text;
        }
    }
}