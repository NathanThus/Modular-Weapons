using System;
using System.Collections.Generic;
using ModularWeapons.UI;
using ModularWeapons.Weapon;
using UnityEditor.Graphs;
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
        [SerializeField] private AmmoUIElement _ammoUI;

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

            _guns[(int)WeaponSlot.Primary].OnFire += HandleWeaponFire;
            _guns[(int)WeaponSlot.Primary].OnReload += HandleWeaponReload;
        }

        private void OnDestroy()
        {
            _switchAction.performed -= HandleWeaponSwitch;

            var index = (int)_slot;

            _guns[index].OnFire -= HandleWeaponFire;
            _guns[index].OnReload -= HandleWeaponReload;
        }

        #endregion

        #region Private

        private void SwitchWeapon()
        {
            var index = (int)_slot;

            _guns[index].OnFire -= HandleWeaponFire;
            _guns[index].OnReload -= HandleWeaponReload;
            _guns[index].gameObject.SetActive(false);

            _slot = SwitchSlot(_slot);
            index = (int)_slot;

            _guns[index].OnFire += HandleWeaponFire;
            _guns[index].OnReload += HandleWeaponReload;
            _guns[index].gameObject.SetActive(true);
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

        private void HandleWeaponReload(int magazine, int reserve)
        {
            _ammoUI.UpdateUI(magazine, reserve);
        }

        private void HandleWeaponFire(int magazine)
        {
            _ammoUI.UpdateUI(magazine);
        }

        #endregion

    }
}
