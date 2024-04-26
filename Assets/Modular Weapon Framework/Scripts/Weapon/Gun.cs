using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ModularWeapons.Audio;
using ModularWeapons.Bullet;
using ModularWeapons.Spread;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace ModularWeapons.Weapon
{
    public class Gun : MonoBehaviour
    {
        #region Nested Entities

        [Serializable]
        private struct RecoilConfiguration
        {
            [Tooltip("Minimum and Maximum horizontal recoil")]
            [SerializeField] private Vector2 _horizontalRecoil;
            [Tooltip("Minimum and Maximum vertical recoil")]
            [SerializeField] private Vector2 _verticalRecoil;

            public Vector2 HorizontalRecoil => _horizontalRecoil;
            public Vector2 VerticalRecoil => _verticalRecoil;
        }

        #endregion

        #region Serialized Fields
        [Header("Gun Stats")]
        [SerializeField] private float _damage = 25;

        [Tooltip("Rate of fire, in Rounds Per Minute")]
        [SerializeField] private float _fireRate = 60;

        [Tooltip("Delay before firing the gun, in seconds")]
        [SerializeField] private float _fireDelaySeconds = 0;

        [Tooltip("Number of pellets per shot fired")]
        [SerializeField] private float _pellets = 1;

        [Header("Magazine")]
        [SerializeField] private Magazine _magazine = new();

        [Header("Spread")]

        [Tooltip("The spread pattern for the bullets")]
        [SerializeField] private BulletSpread _spread;

        [Header("Recoil")]
        [SerializeField] private RecoilConfiguration _recoil;

        [Header("Visuals")]
        [SerializeField] private ParticleSystem _muzzleFlash;

        [Header("Sound")]
        [SerializeField] private GunAudio _firingAudio;
        [SerializeField] private GunAudio _reloadAudio;

        [Header("Script Dependencies")]
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private BulletTemplate _bullet;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _camera;

        #endregion
        #region Fields

        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private bool _canFire = true;
        private InputAction _fireAction;
        private InputAction _reloadAction;

        #endregion

        #region Properties

        public float DelayBetweenShotsSeconds { get { return 60 / _fireRate; } }

        #endregion

        #region Start

        private void Start()
        {
            _fireAction = _playerInput.actions.FindAction("Fire");
            if (_fireAction == null) throw new ArgumentNullException(nameof(_fireAction));

            _reloadAction = _playerInput.actions.FindAction("Reload");
            if (_reloadAction == null) throw new ArgumentNullException(nameof(_reloadAction));

            if (_spread == null) throw new ArgumentNullException(nameof(_spread),
                                                                "Did you forget to assign bullet spread?");

            if(_camera == null) throw new ArgumentNullException(nameof(_camera));

            _bullet.AssignDamage(_damage);

            SubscribeToPlayerActions();
            _magazine.ForceReload();
        }

        private void OnDestroy()
        {
            UnSubscribeToPlayActions();

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void OnEnable()
        {
            SubscribeToPlayerActions();
        }

        private void OnDisable()
        {
            UnSubscribeToPlayActions();
        }

        #endregion

        #region Public Methods

        public async void Fire(CancellationToken token)
        {
            if(!_canFire) return;

            if(_fireDelaySeconds > 0f)
            {
                await UniTask.WaitForSeconds(_fireDelaySeconds);
            }

            while (_fireAction.IsPressed() && _canFire)
            {
                if (!_magazine.Fire()) return;

                _canFire = false;

                if (_muzzleFlash != null)
                {
                    _muzzleFlash.Play();
                }

                _firingAudio?.PlayRandom();

                Vector2 spread = _spread.GetSpread();

                _bullet.Fire(_muzzleTransform.position, _muzzleTransform.forward, spread);
                ApplyRecoil();
                await UniTask.WaitForSeconds(DelayBetweenShotsSeconds, cancellationToken: token);

                _canFire = true;
            }
        }

        public async void Reload(CancellationToken token)
        {
            _reloadAudio?.PlayRandom();
            await _magazine.Reload(token);
        }

        #endregion

        #region Private

        private void ApplyRecoil()
        {
            Vector3 recoil = new(Random.Range(_recoil.HorizontalRecoil.x, _recoil.HorizontalRecoil.y),
                                 Random.Range(_recoil.VerticalRecoil.x, _recoil.VerticalRecoil.y),
                                 0);
            _camera.rotation.SetLookRotation(recoil);
        }

        private void HandleFirePress(InputAction.CallbackContext _)
        {
            try
            {
                Fire(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                throw new OperationCanceledException("Fire was cancelled because the operation was cancelled!");
            }
        }

        private void HandleReloadPress(InputAction.CallbackContext context)
        {
            try
            {
                Reload(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                throw new OperationCanceledException("Fire was cancelled because the operation was cancelled!");
            }
        }

        private void UnSubscribeToPlayActions()
        {
            _fireAction.performed -= HandleFirePress;
            _reloadAction.performed -= HandleReloadPress;
        }

        private void SubscribeToPlayerActions()
        {
            if(_fireAction == null || _reloadAction == null) return;
            _fireAction.performed += HandleFirePress;
            _reloadAction.performed += HandleReloadPress;
        }

        #endregion
    }

}