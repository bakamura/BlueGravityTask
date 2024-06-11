using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BGTask {
    [RequireComponent (typeof(Tilemap))]
    public class Roof : MonoBehaviour {

        [Header("Parameters")]

        [SerializeField, Range(0f, 1f)] private float _visibilityInside;
        [SerializeField, Min(0)] private float _fadeDuration;

        [Header("Cache")]

        private Tilemap _tilemap;
        private float _targetAlpha ;
        private bool _fading = false;

        private void Awake() {
            _tilemap = GetComponent<Tilemap>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            _targetAlpha = _visibilityInside;
            if (!_fading) StartCoroutine(Fade());
        }

        private void OnTriggerExit2D(Collider2D collision) {
            _targetAlpha = 1f;            
            if (!_fading) StartCoroutine(Fade());
        }

        private IEnumerator Fade() {
            _fading = true;
            Color tilemapColor = _tilemap.color;
            while (_tilemap.color.a != _targetAlpha) {
                tilemapColor.a += Mathf.Sign(_targetAlpha - tilemapColor.a) * Time.deltaTime / _fadeDuration;
                if(tilemapColor.a < _visibilityInside || tilemapColor.a > 1f) tilemapColor.a = _targetAlpha;
                _tilemap.color = tilemapColor;

                yield return null;
            }
            _fading = false;
        }

    }
}
