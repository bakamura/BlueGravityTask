using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    public static T Instance { get; private set; }

    protected virtual void Awake() {
        if (Instance == null) Instance = this as T;
        else if (Instance != this) {
            Debug.LogWarning($"Multiple instances of {typeof(T).Name}, destroying object '{gameObject.name}'");
            Destroy(gameObject);
        }
    }

}
