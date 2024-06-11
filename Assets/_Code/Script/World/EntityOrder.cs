using UnityEngine;

namespace BGTask {
    public class EntityOrder : MonoBehaviour {

        [Header("Parameters")]

        [SerializeField] private int _offset;

        [Header("Cache")]

        private SpriteRenderer _spriteRenderer;

        void Update() {
            _spriteRenderer.sortingOrder = (int)transform.position.y + _offset;
        }
    }
}
