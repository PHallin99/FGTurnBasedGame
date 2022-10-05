using TMPro;
using UnityEngine;

namespace UI
{
    public class StatusText : MonoBehaviour
    {
        [SerializeField] private TMP_Text textMeshPro;

        public void UpdateText(string text)
        {
            textMeshPro.text = $"Stamina: {text}";
        }
    }
}