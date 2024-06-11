using UnityEngine;

namespace Ivayami.UI {
    public class Quit : MonoBehaviour {

        public void QuitGame() {
            Debug.Log($"Quitting Game");
            Application.Quit();
        }

    }
}