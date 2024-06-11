using UnityEngine;

namespace BGTask {
    public class Quit : MonoBehaviour {

        public void QuitGame() {
            Debug.Log($"Quitting Game");
            Application.Quit();
        }

    }
}