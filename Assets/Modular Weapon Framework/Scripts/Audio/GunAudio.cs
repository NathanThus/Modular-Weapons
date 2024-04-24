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
            if(_source == null) return;

            _source.Play();
        }

        public void PlayRandom()
        {
            if(_source == null) return;
            _source.pitch = UnityEngine.Random.Range(_lowerPitchLimit, _upperPitchLimit);
            _source.Play();
            _source.pitch = 1;
        }
    }
}
