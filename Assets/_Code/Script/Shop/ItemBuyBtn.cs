using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBuyBtn : MonoBehaviour {

    [Header("References")]

    [SerializeField] private Image _preview;
    [SerializeField] private TextMeshProUGUI _price;
    [field: SerializeField] public Button Button { get; private set; }

    public void Setup(string id, int price) {
        _preview.sprite = Resources.Load<Sprite>($"Clothing/{id}");
        _price.text = $"{price}";
    }

}
