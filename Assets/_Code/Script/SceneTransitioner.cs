using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Naka.UI;

namespace BGTask {
    public class SceneTransitioner : Singleton<SceneTransitioner> {

        [Header("UI References")]

        [SerializeField] private Menu _fadeMenu;

        [Header("Cache")]

        private WaitForSeconds _fadeWait;

        protected override void Awake() {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            _fadeWait = new WaitForSeconds(_fadeMenu.TransitionDuration);

            _fadeMenu.Close();
        }

        public void TransitionTo(string sceneToTransition) {
            StartCoroutine(TransitionRoutine(sceneToTransition));   
        }

        private IEnumerator TransitionRoutine(string sceneToTransition) {
            //string sceneToUnload = SceneManager.GetActiveScene().name;
            AsyncOperation aOperation = SceneManager.LoadSceneAsync(sceneToTransition);
            aOperation.allowSceneActivation = false;
            _fadeMenu.Open();

            yield return _fadeWait;

            aOperation.allowSceneActivation = true;

            yield return aOperation;

            //SceneManager.UnloadSceneAsync(sceneToUnload);
            _fadeMenu.Close();
        }

    }
}
