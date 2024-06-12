using UnityEngine;

namespace BGTask {
    public class EntityOrder : MonoBehaviour {

        [Header("Parameters")]

        [SerializeField] private int _offset;

        [Header("Cache")]

        private SpriteRenderer _spriteRenderer;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update() {
            _spriteRenderer.sortingOrder =  (int)(-10f * transform.position.y) + _offset;
        }
    }
}
