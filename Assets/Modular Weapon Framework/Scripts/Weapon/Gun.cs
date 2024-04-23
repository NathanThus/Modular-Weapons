using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        [Header("Recoil & Spread")]

        [Tooltip("Minimum and Maximum horizontal recoil")]
        [SerializeField] private Vector2 _horizontalRecoil = new(0,1);
        [Tooltip("Minimum and Maximum vertical recoil")]
        [SerializeField] private Vector2 _verticalRecoil = new(0,1);

        [Tooltip("The spread pattern for the bullets")]
        [SerializeField] private Component _spread;

        [Header("Visuals")]
        [SerializeField] private ParticleSystem _muzzleFlash;

        [Header("Sound")]
        [SerializeField] private AudioClip _firingAudio;
        [SerializeField] private AudioClip _reloadAudio;

        [Header("Script Dependencies")]
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private PlayerInput _playerInput;

        #endregion
        #region Fields
        
        private readonly CancellationTokenSource _tokenSource = new();
        private bool _canFire = true;
        private InputAction _fireAction;

        #endregion

        #region Properties

        public float DelayBetweenShotsSeconds { get { return 60 / _fireRate; } }

        #endregion

        #region Start

        private void Start()
        {
            _fireAction = _playerInput.actions.FindAction("Fire");
            if(_fireAction == null) throw new ArgumentNullException(nameof(_fireAction));

            _fireAction.performed += HandleFirePress;
        }

        private void OnDestroy()
        {
            _fireAction.performed -= HandleFirePress;

            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        #endregion

        #region Public Methods

        public async void Fire(CancellationToken token)
        {
            while(_fireAction.IsPressed() && _canFire)
            {
                _canFire = false;

                if(_muzzleFlash != null)
                {
                    _muzzleFlash.Play();
                }

                Debug.Log("PEW");

                await UniTask.WaitForSeconds(DelayBetweenShotsSeconds, cancellationToken: token);

                _canFire = true;
            }
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private

        private void HandleFirePress(InputAction.CallbackContext _)
        {
            Fire(_tokenSource.Token);
        }

        #endregion
    }

}