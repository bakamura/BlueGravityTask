using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace BGTask {
    public class Pause : Singleton<Pause> {

        [Header("Events")]

        public UnityEvent onPause = new UnityEvent();
        public UnityEvent onUnpause = new UnityEvent();

        [Header("Inputs")]

        [SerializeField] private InputActionReference _pauseInput;
        [SerializeField] private InputActionReference _unpauseInput;

        private void Start() {
            _pauseInput.action.started += (callBackContext) => PauseGame(true);
            _unpauseInput.action.started += (callBackContext) => PauseGame(false);
        }

        public void PauseGame(bool isPausing) {
            (isPausing ? onPause : onUnpause)?.Invoke();

            Debug.Log($"Game Pause: {isPausing}");
        }

    }
}