using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace BGTask {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : Singleton<PlayerMovement> {

        public UnityEvent<Vector2> onMoveDirectionChange { get; private set; } = new UnityEvent<Vector2>();

        [Header("Input")]

        [SerializeField] private InputActionReference _movementInput;

        [Header("Parameters")]

        [SerializeField, Min(0)] private float _movementSpeed;

        [Header("Cache")]

        private Vector2 _inputDirection;

        private Rigidbody2D _rigidbody;

        protected override void Awake() {
            base.Awake();

            _movementInput.action.actionMap.Enable();
            _movementInput.action.performed += MoveDirection;
            _movementInput.action.canceled += MoveDirection;
            
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            Move();            
        }

        private void Move() {
            _rigidbody.velocity = _inputDirection * _movementSpeed;
        }

        private void MoveDirection(InputAction.CallbackContext context) {
            _inputDirection = context.ReadValue<Vector2>();
            onMoveDirectionChange?.Invoke(_inputDirection);

            Debug.Log($"Movement Direction Changed to {_inputDirection}");
        }

    }
}
