using UnityEngine.Audio;
using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Simple audio manager for the purpose of playing quack and some random squeeze SFXs. Extends MonoSingleton
    /// </summary>
    public class SoundManager : MonoSingleton<SoundManager>
    {
        #region Inspector fields
        [SerializeField]
        private Sound quackSound;

        [SerializeField]
        private Sound[] squeezeSounds;
        #endregion

        #region Caching SFXs
        private void Awake()
        {
            foreach (Sound singleSound in squeezeSounds)
            {
                singleSound.source = gameObject.AddComponent<AudioSource>();
                singleSound.source.clip = singleSound.soundClip;
            }

            quackSound.source = gameObject.AddComponent<AudioSource>();
            quackSound.source.clip = quackSound.soundClip;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Plays random squeeze SFX with random pitch and volume (limited by values in corresponding Sound object)
        /// </summary>
        /// <param name="source">optional AudioSource, if not delivered then uses source from Sound instance</param>
        public void PlayRandomSqueeze(AudioSource source = null)
        {
            Sound randomSqueeze = squeezeSounds[Random.Range(0, squeezeSounds.Length)];
            PlaySound(randomSqueeze, true, source);
        }

        /// <summary>
        /// Plays Duckie quack SFX with pitch and volume defined by a corresponding Sound object
        /// </summary>
        /// <param name="source">optional AudioSource, if not delivered then uses source from Sound instance</param>
        public void PlayQuack(AudioSource source = null)
        {
            PlaySound(quackSound, false, source);
        }

        /// <summary>
        /// Plays sound given in the first parameter
        /// </summary>
        /// <param name="soundToPlay"></param>
        /// <param name="isRandomVolumeAndPitch">optional bool, default = false</param>
        /// <param name="source">optional AudioSource, if not delivered then uses source from Sound instance</param>
        public void PlaySound(Sound soundToPlay, bool isRandomVolumeAndPitch = false, AudioSource source = null )
        {
            if (source != null)
            {
                soundToPlay.source = source;
            }

            soundToPlay.source.volume = !isRandomVolumeAndPitch ? soundToPlay.volume : Random.Range(.1f, soundToPlay.volume);
            soundToPlay.source.pitch = !isRandomVolumeAndPitch ? soundToPlay.pitch : Random.Range(.1f, soundToPlay.pitch);

            soundToPlay.source.Play();
        }
        #endregion
    }
}
