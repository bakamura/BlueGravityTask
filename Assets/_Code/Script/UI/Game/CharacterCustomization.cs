using System.Collections.Generic;
using UnityEngine;

namespace BGTask {
    public class CharacterCustomization : MonoBehaviour {

        [Header("References")]

        [SerializeField] private ItemEquipBtn _itemEquipBtnPrefab;
        [SerializeField] private Transform _itemEquipContainer;

        [Header("Cache")]

        private List<ItemEquipBtn> _itemEquipBtns = new List<ItemEquipBtn>();

        public void UpdateEquipAvailable() {
            Clothing[] availableClothes = PlayerInventory.Instance.ReadClothes();
            for (int i = 0; i < availableClothes.Length; i++) {
                Clothing clothing = availableClothes[i];
                if (i >= _itemEquipBtns.Count) _itemEquipBtns.Add(Instantiate(_itemEquipBtnPrefab, _itemEquipContainer));
                else _itemEquipBtns[i].gameObject.SetActive(true);
                _itemEquipBtns[i].Setup(clothing.Id);
                _itemEquipBtns[i].Button.onClick.RemoveAllListeners();
                _itemEquipBtns[i].Button.onClick.AddListener(() => PlayerAnimation.Instance.UpdateAccessorySprites(clothing));
            }
            for (int i = availableClothes.Length; i < _itemEquipBtns.Count; i++) _itemEquipBtns[i].gameObject.SetActive(false);
        }

    }
}