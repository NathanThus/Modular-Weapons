using System;
using System.Collections.Generic;
using ModularWeapons.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularWeapons.Manager
{
    public class WeaponManager : MonoBehaviour
    {
        #region Nested Entities

        private enum WeaponSlot
        {
            Primary = 0,
            Secondary = 1
        }

        #endregion

        #region Serialized Fields

        [SerializeField] private List<Gun> _guns;
        [SerializeField] private PlayerInput _playerInput;

        #endregion

        #region Fields

        private WeaponSlot _slot = WeaponSlot.Primary;
        private InputAction _switchAction;


        #endregion

        #region Start

        private void Start()
        {
            _switchAction = _playerInput.actions.FindAction("Switch Weapon");
            if (_switchAction == null) throw new ArgumentNullException(nameof(_switchAction), "The action was not found!");

            _switchAction.performed += HandleWeaponSwitch;
        }

        private void OnDestroy()
        {
            _switchAction.performed -= HandleWeaponSwitch;
        }

        #endregion

        #region Private

        private void SwitchWeapon()
        {
            _guns[(int)_slot].gameObject.SetActive(false);
            _slot = SwitchSlot(_slot);
            _guns[(int)_slot].gameObject.SetActive(true);
        }


        private WeaponSlot SwitchSlot(WeaponSlot slot) => slot switch
        {
            WeaponSlot.Primary => WeaponSlot.Secondary,
            WeaponSlot.Secondary => WeaponSlot.Primary,
            _ => throw new NotImplementedException(nameof(slot))
        };

        private void HandleWeaponSwitch(InputAction.CallbackContext context)
        {
            SwitchWeapon();
        }

        #endregion

    }
}
