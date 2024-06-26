
using UnityEngine;

namespace Naka.UI {
    public class SceneTransition : Fade {

        public static SceneTransition Instance { get; private set; }

        private MenuGroup _menuGroup;
        public Menu Menu { get; private set; }
        public AnimationCurve TransitionCurve => _transitionCurve;

        protected override void Awake() {
            if (Instance == null) Instance = this;
            else if (Instance != this) {
                Debug.LogWarning($"Multiple instances of {typeof(SceneTransition).Name}, destroying object '{gameObject.name}'");
                Destroy(gameObject);
            }

            base.Awake();
        }

        private void Start() {
            _menuGroup = GetComponent<MenuGroup>();
            Menu = GetComponent<Menu>();
            _menuGroup.CloseCurrentThenOpen(Menu);
        }

        public void Transition() {
            Debug.Log($"Scene Transition Fade");
            _menuGroup.CloseCurrentThenOpen(Menu);
        }

        public void SetDuration(float durationSeconds) {
            _transitionDuration = durationSeconds;
        }

        public void SetAnimationCurve(AnimationCurve animCurve)
        {
            _transitionCurve = animCurve;
        }

    }
}
