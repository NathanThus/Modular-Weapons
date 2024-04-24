using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ModularWeapons.Weapon
{
    [Serializable]
    public class Magazine
    {
        [SerializeField] private int _magazineSize;
        [SerializeField] private int _remainingBullets;

        [SerializeField] private float _reloadTime;
        
        [SerializeField] private bool _loadSingleBullet;

        private bool _isReloading;

        public async void Reload(CancellationToken token)
        {
            if(_magazineSize == _remainingBullets) return;
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