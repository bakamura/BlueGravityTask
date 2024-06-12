using UnityEngine;

namespace BGTask {
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : Singleton<PlayerAnimation> {

        [Header("References")]

        [SerializeField] private SpriteRenderer[] _accessoryRenderers;

        [Header("Cache")]

        private Animator _animator;
        private SpriteRenderer _baseRenderer;
        private int _currentFrame;
        private int _frameIterator;
        private Sprite[,] _accessoriesSprites;

        private static int MOVEMENT_BOOL_PARAMETER = Animator.StringToHash("IsMoving");
        private static int MOVEMENT_X_PARAMETER = Animator.StringToHash("MovementX");
        private static int MOVEMENT_Y_PARAMETER = Animator.StringToHash("MovementY");
        //private static int INTERACT_PARAMETER = Animator.StringToHash("Interact");

        protected override void Awake() {
            base.Awake();

            _animator = GetComponent<Animator>();
            _baseRenderer = GetComponent<SpriteRenderer>();
            _accessoriesSprites = new Sprite[_accessoryRenderers.Length, 64]; // Somehow make 64 adjustable to base spriteSheet
        }

        private void Start() {
            PlayerMovement.Instance.OnMoveDirectionChange.AddListener(MoveAnimation);
            //PlayerInteract.Instance.OnInteract.AddListener(InteractAnimation);
        }

        private void Update() {
            _frameIterator = int.Parse(_baseRenderer.sprite.name.Split('_')[2]);
            if (_currentFrame != _frameIterator) UpdateAccessoryRenderers();
        }

        private void MoveAnimation(Vector2 direction) {
            if (direction != Vector2.zero) {
                _animator.SetFloat(MOVEMENT_X_PARAMETER, direction.x);
                _animator.SetFloat(MOVEMENT_Y_PARAMETER, direction.y);
                _animator.SetBool(MOVEMENT_BOOL_PARAMETER, true);
            }
            else _animator.SetBool(MOVEMENT_BOOL_PARAMETER, false);
        }

        //private void InteractAnimation() {
        //    _animator.SetTrigger(INTERACT_PARAMETER);
        //}

        public void UpdateAccessorySprites(Clothing clothing) {
            Sprite[] sprites = clothing.Id != null ? Resources.LoadAll<Sprite>($"Clothing/Sheet/{clothing.Id}_Sheet") : new Sprite[_accessoriesSprites.GetLength(1)];
            for (int i = 0; i < sprites.Length; i++) _accessoriesSprites[(int)clothing.Type, i] = sprites[i];
            UpdateAccessoryRenderers();
        }

        private void UpdateAccessoryRenderers() {
            _currentFrame = _frameIterator;
            for (int i = 0; i < _accessoriesSprites.GetLength(0); i++) _accessoryRenderers[i].sprite = _accessoriesSprites[i, _currentFrame];
        }

        public void ResetAllAccessorySprites() {
            Clothing[] clothingTypes = new Clothing[3] {
                new Clothing(Clothing.Placement.Suit, null),
                new Clothing(Clothing.Placement.Hair, null),
                new Clothing(Clothing.Placement.Hat, null)
            };
            foreach(Clothing clothing in clothingTypes) UpdateAccessorySprites(clothing);
        }

    }
}
