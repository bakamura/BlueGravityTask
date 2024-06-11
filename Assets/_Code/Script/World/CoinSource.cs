using UnityEngine;

namespace BGTask {
    public class CoinSource : MonoBehaviour, IInteractable {

        [Header("Parameters")]

        [SerializeField, Min(0)] private int _addedCoins;

        public void Interact() {
            PlayerInventory.Instance.AddCoins(_addedCoins);
        }

    }
}