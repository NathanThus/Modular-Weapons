using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ModularWeapons.Weapon
{
    [Serializable]
    public class Magazine
    {
        #region Serialized Fields

        [SerializeField] private int _magazineSize;
        [SerializeField] private int _remainingBullets;
        [SerializeField] private int _reserveAmmo;

        [SerializeField] private float _reloadTime;

        [SerializeField] private bool _loadSingleBullet;

        #endregion

        #region Fields

        private bool _isReloading;

        #endregion

        #region Properties

        public int RemainingBullets => _remainingBullets;
        public int ReserveAmmo => _reserveAmmo;

        #endregion

        #region Public

        /// <summary>
        /// Reloads the magazine with new bullets. Will reload the entire mag, or as much available ammo as possible.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async UniTask Reload(CancellationToken token)
        {
            await Reload(token, _magazineSize - _remainingBullets);
        }

        /// <summary>
        /// Reloads the magazine with new bullets. Will reload the designated amount of bullets, or as much available ammo as possible.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <param name="bullets">The number of bullets to reload</param>
        /// <returns>A Task.</returns>
        public async UniTask Reload(CancellationToken token, int bullets)
        {
            if (_magazineSize == _remainingBullets) return;
            if (_reserveAmmo == 0) return;
            if (_isReloading) return;

            _isReloading = true;

            await UniTask.WaitForSeconds(_reloadTime, cancellationToken: token);

            if(bullets > _reserveAmmo)
            {
                _remainingBullets = _reserveAmmo;
                _reserveAmmo = 0;
            }
            else
            {
                _remainingBullets = _magazineSize;
                _reserveAmmo -= bullets;
            }


            _isReloading = false;
        }

        public void ForceReload()
        {
            _remainingBullets = _magazineSize;
        }

        public bool Fire()
        {
            if (_remainingBullets <= 0) return false;

            _remainingBullets--;
            return true;
        }

        public bool Fire(int bullets)
        {
            if (_remainingBullets <= 0 || bullets > _remainingBullets) return false;

            _remainingBullets -= bullets;
            return true;
        }

        #endregion
    }
}