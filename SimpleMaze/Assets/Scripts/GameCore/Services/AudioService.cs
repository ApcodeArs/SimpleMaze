using System.Collections.Generic;
using System.Linq;
using Models.Audio;
using UnityEngine;

namespace GameCore.Services {
    public class AudioService: MonoBehaviourCoreService {
        //todo change to custom serialized dictionary
        [SerializeField] private List<AudioData> _clips;

        public void PlaySound(SoundId ident, float delay = 0, float volume = 1, bool isLoop = false) {
            var clip = FindClip(ident);

            if (clip == null) {
                Debug.LogWarning($"AudioClip {ident} not found");
                return;
            }

            var audioSource = InitAudioSource(clip, volume, isLoop);
            Play(audioSource, delay);
        }

        private AudioClip FindClip(SoundId ident) {
            var clipData = _clips.FirstOrDefault(audioData => audioData.Ident.Equals(ident));
            return clipData?.Source;
        }
        
        private AudioSource InitAudioSource(AudioClip clip, float volume, bool isLoop) {
            var audioSource = gameObject.AddComponent<AudioSource>(); //todo possible improvement (objects pool pattern)
            
            audioSource.playOnAwake = false;
            audioSource.loop = isLoop;
            audioSource.clip = clip;
            audioSource.volume = volume;

            return audioSource;
        }

        private void Play(AudioSource audioSource, float delay) {
            audioSource.PlayDelayed(delay);
        
            if (!audioSource.loop){
                Destroy(audioSource, audioSource.clip.length + delay + 0.05f);
            }
        }
    }
}