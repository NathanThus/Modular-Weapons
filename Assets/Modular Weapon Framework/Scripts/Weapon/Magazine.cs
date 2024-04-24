using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ModularWeapons.Weapon
{
    [Serializable]
    public struct Magazine
    {
        [SerializeField] private int _magazineSize;
        [SerializeField] private int _remainingBullets;

        [SerializeField] private float _reloadTime;
        
        [SerializeField] private bool _loadSingleBullet;

        private bool _isReloading;

        public readonly int MagazineSize => _magazineSize;
        public readonly int RemainingBullets => _remainingBullets;

        public async void Reload(CancellationToken token)
        {
            if(_isReloading) return;
            _isReloading = true;

            await UniTask.WaitForSeconds(_reloadTime, cancellationToken: token);

            _remainingBullets = _magazineSize;
            _isReloading = false;
        }

        public void ForceReload()
        {
            _remainingBullets = _magazineSize;
        }

        public bool Fire()
        {
            if(_remainingBullets <= 0) return false;
            
            _remainingBullets--;
            return true;
        }

        public bool Fire(int bullets)
        {
            if(_remainingBullets <= 0 || bullets > _remainingBullets) return false;

            _remainingBullets -= bullets;
            return true;
        }
    }
}