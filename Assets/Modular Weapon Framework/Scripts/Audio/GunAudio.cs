using System;
using UnityEngine;

namespace ModularWeapons.Audio
{
    [Serializable]
    public class GunAudio
    {
        [SerializeField] private AudioSource _source;
        [SerializeField, Range(1, 10)] private float _upperPitchLimit;
        [SerializeField, Range(-9, 1)] private float _lowerPitchLimit;
        [SerializeField]

        public void Play()
        {
            _source.Play();
        }

        public void PlayRandom()
        {
            _source.pitch = UnityEngine.Random.Range(_lowerPitchLimit, _upperPitchLimit);
            _source.Play();
            _source.pitch = 1;
        }
    }
}
