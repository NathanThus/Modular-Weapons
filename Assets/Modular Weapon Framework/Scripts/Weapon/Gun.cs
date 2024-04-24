using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using ModularWeapons.Audio;
using ModularWeapons.Spread;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularWeapons.Weapon
{
    public class Gun : MonoBehaviour
    {
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

        [Header("Recoil & Spread")]

        [Tooltip("Minimum and Maximum horizontal recoil")]
        [SerializeField] private Vector2 _horizontalRecoil = new(0, 1);
        [Tooltip("Minimum and Maximum vertical recoil")]
        [SerializeField] private Vector2 _verticalRecoil = new(0, 1);

        [Tooltip("The spread pattern for the bullets")]
        [SerializeField] private BulletSpread _spread;

        [Header("Visuals")]
        [SerializeField] private ParticleSystem _muzzleFlash;

        [Header("Sound")]
        [SerializeField] private GunAudio _firingAudio;
        [SerializeField] private GunAudio _reloadAudio;

        [Header("Script Dependencies")]
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private PlayerInput _playerInput;

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

            if(_spread == null) throw new ArgumentNullException(nameof(_spread),
                                                                "Did you forget to assign bullet spread?");

            _fireAction.performed += HandleFirePress;
            _reloadAction.performed += HandleReloadPress;

            _magazine.ForceReload();
        }

        private void OnDestroy()
        {
            _fireAction.performed -= HandleFirePress;
            _reloadAction.performed -= HandleReloadPress;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        #endregion

        #region Public Methods

        public async void Fire(CancellationToken token)
        {
            while (_fireAction.IsPressed() && _canFire)
            {
                if (!_magazine.Fire()) return;

                _canFire = false;

                if (_muzzleFlash != null)
                {
                    _muzzleFlash.Play();
                }

                _firingAudio.PlayRandom();

                Vector2 spread = _spread.GetSpread();
                spread *= Vector2.zero;

                Debug.Log("PEW");

                await UniTask.WaitForSeconds(DelayBetweenShotsSeconds, cancellationToken: token);

                _canFire = true;
            }
        }

        public void Reload(CancellationToken token)
        {
            _reloadAudio.PlayRandom();
            _magazine.Reload(token);
        }

        #endregion

        #region Private

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

        #endregion
    }

}