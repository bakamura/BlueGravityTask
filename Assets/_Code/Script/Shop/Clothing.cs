using UnityEngine;

[System.Serializable]
public class Clothing {

    public enum Placement {
        Suit,
        Hair,
        Hat
    }

    [field: SerializeField] public Placement Type { get; private set; }
    [field: SerializeField] public string Id { get; private set; }

}
