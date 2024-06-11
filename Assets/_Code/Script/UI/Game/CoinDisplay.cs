using UnityEngine;
using TMPro;

namespace BGTask {
    public class CoinDisplay : MonoBehaviour {

        [Header("References")]

        [SerializeField] private TextMeshProUGUI _textDisplay;

        private void Start() {
            PlayerInventory.Instance.onCoinAmountChange.AddListener(UpdateDisplay);
        }

        private void UpdateDisplay(int amount) {
            _textDisplay.text = amount.ToString();
        }

    }
}
