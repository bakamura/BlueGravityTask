using UnityEngine;
using UnityEngine.InputSystem;

namespace BGTask {
    public class PlayerMovement : MonoBehaviour {

        [Header("Input")]

        [SerializeField] private InputActionReference _movementInput;

        [Header("Parameters")]

        [SerializeField, Min(0)] private float _movementSpeed;

        [Header("Cache")]

        private Vector2 _inputDirection;

        private void Awake() {
            _movementInput.action.performed += Move;
        }

        private void Move(InputAction.CallbackContext context) {
            _inputDirection = context.ReadValue<Vector2>();
            transform.Translate(_inputDirection * _movementSpeed * Time.deltaTime);
            Debug.Log("MOVEINPUT");
        }

    }
}
