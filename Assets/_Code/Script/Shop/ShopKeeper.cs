using UnityEngine;
using Naka.UI;

namespace BGTask {
    public class ShopKeeper : MonoBehaviour, IInteractable {

        [Header("References")]

        [SerializeField] private MenuGroup _menuGroup;
        [SerializeField] private Menu _shopMenu;

        public void Interact() {
            InputChanger.Instance.ChangeInputMap("Menu");
            _menuGroup.CloseCurrentThenOpen(_shopMenu);
            _shopMenu.GetComponent<Shop>().UpdateBuyAvailability();
        }

    }
}
