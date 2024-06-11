using System.Collections;
using UnityEngine;

namespace BGTask {
    public class Door : MonoBehaviour, IInteractable {

        [Header("Parameters")]

        [SerializeField, Min(0)] private float _delayToClose;
        private bool _open;
        private bool _canClose;

        [Header("Cache")]

        private Collider2D _collider;
        private Animator _animator;

        private WaitForSeconds _closeWait;

        private static int OPEN_PARAMETER = Animator.StringToHash("Open");
        private static int CLOSE_PARAMETER = Animator.StringToHash("Close");

        private void Awake() {
            _collider = GetComponent<Collider2D>();
            _animator = GetComponentInChildren<Animator>();

            _closeWait = new WaitForSeconds(_delayToClose);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            _canClose = false;
        }

        private void OnTriggerExit2D(Collider2D collision) {
            _canClose = true;
        }

        public void Interact() {
            if (!_open) {
                ToggleDoor(true);
                StartCoroutine(DelayClose());
            }
        }

        public void ToggleDoor(bool isOpen) {
            _open = isOpen;
            _collider.isTrigger = isOpen;
            _animator.SetTrigger(isOpen ? OPEN_PARAMETER : CLOSE_PARAMETER);
        }

        private IEnumerator DelayClose() {
            yield return _closeWait;
            while (!_canClose) yield return null;

            ToggleDoor(false);
        }

    }
}
