using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGTask {
    public class Shop : MonoBehaviour {

        [Header("References")]

        [SerializeField] private ItemBuyBtn _buyBtnPrefab;
        [SerializeField] private Transform _buyBtnsContainer;
        [SerializeField] private Transform _sellBtnsContainer;

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
        private List<ItemBuyBtn> _sellBtns = new List<ItemBuyBtn>();

        private void Awake() {
            InstantiateBuyBtns();
        }

        private void InstantiateBuyBtns() {
            _buyBtns = new ItemBuyBtn[_soldClothes.Count];
            for (int i = 0; i < _buyBtns.Length; i++) {
                _buyBtns[i] = Instantiate(_buyBtnPrefab, _buyBtnsContainer);
                _buyBtns[i].Setup(_soldClothes[i].Clothing.Id, _soldClothes[i].BuyPrice);
                int id = i;
                _buyBtns[i].Button.onClick.AddListener(() => Buy(id));
            }
        }

        public void UpdateBuyAvailability() {
            for (int i = 0; i < _buyBtns.Length; i++) _buyBtns[i].Button.interactable = (!PlayerInventory.Instance.HasClothing(_soldClothes[i].Clothing) && PlayerInventory.Instance.CanPay(_soldClothes[i].BuyPrice));
        }

        public void UpdateSellAvailability() {
            Clothing[] sellableClothes = PlayerInventory.Instance.ReadClothes();
            for (int i = 0; i < sellableClothes.Length; i++) {
                ShopItem shopItemRef = _soldClothes.FirstOrDefault(shopItem => shopItem.Clothing.Id == sellableClothes[i].Id);
                if (i >= _sellBtns.Count) _sellBtns.Add(Instantiate(_buyBtnPrefab, _sellBtnsContainer));
                else _sellBtns[i].gameObject.SetActive(true);
                _sellBtns[i].Setup(shopItemRef.Clothing.Id, shopItemRef.SellPrice);
                _sellBtns[i].Button.onClick.RemoveAllListeners();
                _sellBtns[i].Button.onClick.AddListener(() => Sell(shopItemRef.Clothing));
            }
            for(int i = sellableClothes.Length; i < _sellBtns.Count; i++) _sellBtns[i].gameObject.SetActive(false);
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
            UpdateSellAvailability();

            Debug.Log($"Sold '{clothing.Id}'");
        }

        public void Close() {
            InputChanger.Instance.ChangeInputMap("Player");
        }

    }
}
