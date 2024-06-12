using UnityEngine;
using UnityEngine.UI;

namespace BGTask {
    public class ItemEquipBtn : MonoBehaviour {

        [Header("References")]

        [SerializeField] private Image _preview;
        public Button Button { get; private set; }

        private void Awake() {
            Button = GetComponent<Button>();
        }

        public void Setup(string id) {
            _preview.sprite = Resources.Load<Sprite>($"Clothing/{id}");
        }

    }
}
