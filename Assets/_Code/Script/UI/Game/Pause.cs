using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace BGTask {
    public class Pause : Singleton<Pause> {

        [field: SerializeField] public UnityEvent OnPause { get; private set; } = new UnityEvent();
        [field: SerializeField] public UnityEvent OnUnpause { get; private set; } = new UnityEvent();

        [Header("Inputs")]

        [SerializeField] private InputActionReference _pauseInput;
        [SerializeField] private InputActionReference _unpauseInput;

        private void Start() {
            _pauseInput.action.started += (callBackContext) => PauseGame(true);
            _unpauseInput.action.started += (callBackContext) => PauseGame(false);
        }

        public void PauseGame(bool isPausing) {
            (isPausing ? OnPause : OnUnpause)?.Invoke();

            Debug.Log($"Game Pause: {isPausing}");
        }

    }
}