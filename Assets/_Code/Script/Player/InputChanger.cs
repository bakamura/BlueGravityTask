using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGTask {
    public class InputChanger : Singleton<InputChanger> {

        [Header("References")]

        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private InputActionReference _unPauseInput;
        private InputActionMap _menuActionMap;

        [Header("Cache")]

        private InputActionMap _inputActionMap;

        protected override void Awake() {
            base.Awake();

            _menuActionMap = _inputActionAsset.actionMaps.FirstOrDefault(actionMap => actionMap.name == "Menu");
            ChangeInputMap("Player");
        }

        private void Start() {
            Pause.Instance.OnPause.AddListener(() => ChangeInputMap("Menu"));
            Pause.Instance.OnUnpause.AddListener(() => ChangeInputMap("Player"));
        }

        public void ChangeInputMap(string mapId) {
            if (_inputActionMap != _menuActionMap) _inputActionMap?.Disable(); // Should only prevent disable when in PC
            else _unPauseInput.action.Disable();
            if (mapId != null) {
                _inputActionMap = _inputActionAsset.actionMaps.FirstOrDefault(actionMap => actionMap.name == mapId);
                _inputActionMap.Enable();
                if (_inputActionMap == _menuActionMap) _unPauseInput.action.Enable();
            }
        }


    }
}
