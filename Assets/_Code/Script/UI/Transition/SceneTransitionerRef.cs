using UnityEngine;

namespace BGTask {
    public class SceneTransitionerRef : MonoBehaviour {

        public void TransitionTo(string sceneToTransition) {
            SceneTransitioner.Instance.TransitionTo(sceneToTransition);
        }

    }
}