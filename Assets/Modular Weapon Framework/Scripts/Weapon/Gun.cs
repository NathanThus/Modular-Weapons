using UnityEditor.EditorTools;
using UnityEngine;

namespace ModularWeapons.Weapon
{
    public class Gun : MonoBehaviour
    {
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
    }

}