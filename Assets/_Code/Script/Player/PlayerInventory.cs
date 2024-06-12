using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BGTask {
    public class PlayerInventory : Singleton<PlayerInventory> {

        public UnityEvent<int> onCoinAmountChange { get; private set; } = new UnityEvent<int>();

        [Header("Coins")]

        private int _coinAmount;

        [Header("Clothing")]

        private List<Clothing> _clothes = new List<Clothing>();

        #region COINS
        public bool CanPay(int coinAmount) {
            return _coinAmount >= coinAmount;
        }

        public void AddCoins(int addAmount) {
            if (addAmount > 0) {
                _coinAmount += addAmount;
                onCoinAmountChange.Invoke(_coinAmount);
            }
            else Debug.LogError($"Trying to add negative value to coin amount '{addAmount}'");
        }

        public void RemoveCoins(int removeAmount) {
            if (removeAmount > 0) {
                _coinAmount -= removeAmount;
                onCoinAmountChange.Invoke(_coinAmount);
            }
            else Debug.LogError($"Trying to remove negative value to coin amount '{removeAmount}'");
        }
        #endregion

        #region CLOTHING
        public bool HasClothing(Clothing clothing) {
            return _clothes.Contains(clothing); // Change to check for equivalence instead
        }

        public Clothing[] ReadClothes() {
            return _clothes.ToArray();
        }

        public void AddClothing(Clothing addedClothing) {
            _clothes.Add(addedClothing);
        }

        public void RemoveClothing(Clothing removedClothing) {
            _clothes.Remove(removedClothing);
        }
        #endregion

    }
}
