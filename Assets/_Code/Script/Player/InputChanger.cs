using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGTask {
    public class InputChanger : Singleton<InputChanger> {

        [Header("References")]

        [SerializeField] private InputActionAsset _inputActionAsset;

        [Header("Cache")]

        private InputActionMap _inputActionMap;

        protected override void Awake() {
            base.Awake();

            ChangeInputMap("Player");
            Pause.Instance.OnPause.AddListener(() => ChangeInputMap("Menu"));
            Pause.Instance.OnUnpause.AddListener(() => ChangeInputMap("Player"));
        }

        public void ChangeInputMap(string mapId) {
            _inputActionMap?.Disable();
            if (mapId != null) {
                _inputActionMap = _inputActionAsset.actionMaps.FirstOrDefault(actionMap => actionMap.name == mapId);
                _inputActionMap.Enable();
            }
        }


    }
}
