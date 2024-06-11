using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace BGTask {
    public class PlayerInteract : Singleton<PlayerInteract> {

        public UnityEvent onInteract { get; private set; } = new UnityEvent();

        [Header("Input")]

        [SerializeField] private InputActionReference _interactInput;

        [Header("Parameters")]

        [SerializeField, Min(0)] private float _interactPointDistance;
        [SerializeField, Min(0)] private float _interactPointRange;
        [SerializeField] private LayerMask _interactableLayer;
        private Transform _interactPoint;

        protected override void Awake() {
            base.Awake();

            _interactInput.action.performed += Interact;

            _interactPoint = Instantiate(new GameObject("InteractPoint"), transform).transform;
            _interactPoint.localPosition = _interactPointDistance * Vector3.up;
        }

        private void Start() {
            PlayerMovement.Instance.onMoveDirectionChange.AddListener(MoveInteractPoint);
        }

        private void Interact(InputAction.CallbackContext context) {
            Collider2D[] hits = new Collider2D[3];
            if (Physics2D.OverlapCircleNonAlloc(_interactPoint.position, _interactPointRange, hits, _interactableLayer) > 0) {
                hits.OrderBy(collider => collider != null ? Vector3.Distance(transform.position, collider.transform.position) : 999f).First().GetComponent<IInteractable>().Interact();
                onInteract?.Invoke();
            }
        }

        private void MoveInteractPoint(Vector2 direction) {
            if (direction != Vector2.zero) _interactPoint.localPosition = _interactPointDistance * direction;
        }

    }
}
