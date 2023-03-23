using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers {
    
    public class SoundManager : MonoBehaviour {
       
        public float soundsVolume = 1f;
        public float soundsPitch = 1f;
        public float musicVolume = 1f;
        public float musicPitch = 1f;
        public bool isPlayOnAwakeSounds = false;
        public bool isPlayOnAwakeMusics = false;
        
        private AudioSource[] soundSources;
        private AudioSource soundAudioSource;
        private AudioSource musicAudioSource;

        private void Start() {
            //soundSources = GetComponents<AudioSource>();
            //soundAudioSource = soundSources[0];
            //musicAudioSource = soundSources[1];
            soundAudioSource = GetComponent<AudioSource>();
            musicAudioSource = GetComponentInChildren<AudioSource>();
            // soundAudioSource = soundSources[0].GetComponent<AudioSource>();
            // musicAudioSource = soundSources[1].GetComponent<AudioSource>();
            // soundAudioSource.volume = soundsVolume;
            // soundAudioSource.pitch = soundsPitch;
            // soundAudioSource.playOnAwake = isPlayOnAwakeSounds;
            // musicAudioSource.volume = musicVolume;
            // musicAudioSource.pitch = musicPitch;
            // musicAudioSource.playOnAwake = isPlayOnAwakeMusics;
            soundAudioSource.volume = 0.5f;
            musicAudioSource.volume = 0.3f;
            // if (musicAudioSource==null) {
            //     Debug.Log("NULL!!!!!");
            // }
        }

        // public void PlaySound(AudioClip audioClip) {
        //    // if (soundAudioSource.isPlaying) return;
        //     soundAudioSource.clip = audioClip;
        //     //soundAudioSource.Play();
        //     soundAudioSource.PlayOneShot(audioClip);
        // }

        public void PlaySound(AudioClip audioClip, float volume, float pitch) {
            soundAudioSource.clip = audioClip;
            soundAudioSource.volume = volume;
            soundAudioSource.pitch = pitch;
            //soundAudioSource.Play();
            soundAudioSource.PlayOneShot(audioClip);
        }

        public void PlayMusic(AudioClip audioClip) {
            if (musicAudioSource == null) {
                Debug.Log("NULL");
            }
            musicAudioSource.clip = audioClip;
            musicAudioSource.Play();
        }

        public void PlayMusic(AudioClip audioClip, float volume, float pitch) {
            musicAudioSource.clip = audioClip;
            musicAudioSource.volume = volume;
            musicAudioSource.pitch = pitch;
            musicAudioSource.Play();
        }
    }
}