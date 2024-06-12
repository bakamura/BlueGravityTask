using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGTask {
    public class Shop : MonoBehaviour {

        [Header("References")]

        [SerializeField] private ItemBuyBtn _buyBtnPrefab;
        [SerializeField] private Transform _buyBtnsContainer;

        [Header("Parameters")]

        [SerializeField] private List<ShopItem> _soldClothes;
        [System.Serializable]
        public class ShopItem {
            [field: SerializeField] public Clothing Clothing { get; private set; }
            [field: SerializeField] public int BuyPrice { get; private set; }
            [field: SerializeField] public int SellPrice { get; private set; }
        }

        [Header("Cache")]

        private ItemBuyBtn[] _buyBtns;

        private void Awake() {
            _buyBtns = new ItemBuyBtn[_soldClothes.Count];
            for (int i = 0; i < _buyBtns.Length; i++) {
                _buyBtns[i] = Instantiate(_buyBtnPrefab, _buyBtnsContainer);
                _buyBtns[i].Setup(_soldClothes[i].Clothing.Id, _soldClothes[i].BuyPrice);
                _buyBtns[i].Button.onClick.AddListener(() => {
                    int id = i;
                    Buy(id);
                });
            }
        }

        public void UpdateBuyAvailability() {
            for (int i = 0; i < _buyBtns.Length; i++) _buyBtns[i].enabled = (!PlayerInventory.Instance.HasClothing(_soldClothes[i].Clothing) && PlayerInventory.Instance.CanPay(_soldClothes[i].BuyPrice));
        }

        public void UpdateSellAvailability() {

        }

        public void Buy(int id) {
            PlayerInventory.Instance.RemoveCoins(_soldClothes[id].BuyPrice);
            PlayerInventory.Instance.AddClothing(_soldClothes[id].Clothing);
            UpdateBuyAvailability();

            Debug.Log($"Bought '{_soldClothes[id].Clothing.Id}'");
        }

        public void Sell(Clothing clothing) {
            PlayerInventory.Instance.AddCoins(_soldClothes.FirstOrDefault(shopItem => shopItem.Clothing.Id == clothing.Id).SellPrice);
            PlayerInventory.Instance.RemoveClothing(clothing);
        }

        public void Close() {
            InputChanger.Instance.ChangeInputMap("Player");
        }

    }
}
