using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGTask {
    public class InputChanger : Singleton<InputChanger> {

        [Header("References")]

        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private InputActionMap _menuActionMap;

        [Header("Cache")]

        private InputActionMap _inputActionMap;

        protected override void Awake() {
            base.Awake();

            ChangeInputMap("Player");
        }

        private void Start() {
            Pause.Instance.OnPause.AddListener(() => ChangeInputMap("Menu"));
            Pause.Instance.OnUnpause.AddListener(() => ChangeInputMap("Player"));
        }

        public void ChangeInputMap(string mapId) {
            if(_inputActionMap != _menuActionMap) _inputActionMap?.Disable(); // Should only prevent disable when in PC
            if (mapId != null) {
                _inputActionMap = _inputActionAsset.actionMaps.FirstOrDefault(actionMap => actionMap.name == mapId);
                _inputActionMap.Enable();
            }
        }


    }
}
