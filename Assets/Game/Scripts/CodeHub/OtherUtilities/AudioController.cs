using System.Collections.Generic;
using GameLogic;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private List<AudioSource> sounds;
        [SerializeField] private List<AudioSource> musics;

        [SerializeField] private SettingsButton _buttonSound;
        [SerializeField] private SettingsButton _buttonMusic;
        [SerializeField] private SegmentsActivator _soundSlider;
        [SerializeField] private SegmentsActivator _musicSlider;

        private float modifier = 0.1f;

        private void Start()
        {
            EnableAudioButtons();
            MuteAudio();
          //  SetValueSliders();
          //  SetVolumeSound();
           // SetVolumeMusic();
        }

        private void EnableAudioButtons()
        {
            if (_buttonMusic == null || _buttonSound == null) return;
            _buttonMusic.SetEnable(_playerDatabase.HasEnableMusic);
            _buttonSound.SetEnable(_playerDatabase.HasEnableSound);
        }

        private void MuteAudio()
        {
            foreach (var sound in sounds)
            {
                sound.mute = !_playerDatabase.HasEnableSound;
            }

            foreach (var music in musics)
            {
                music.mute = !_playerDatabase.HasEnableMusic;
            }
        }

        private void SetValueSliders()
        {
            if (_musicSlider == null || _soundSlider == null) return;
            _musicSlider.ActiveElements(_playerDatabase.VolumeMusic);
            _soundSlider.ActiveElements(_playerDatabase.VolumeSound);
        }

        public void EnableSound(bool enable)
        {
            if (_buttonSound != null)
                _buttonSound.SetEnable(enable);

            _playerDatabase.HasEnableSound = enable;
            MuteAudio();
        }

        public void EnableMusic(bool enable)
        {
            if (_buttonSound != null)
                _buttonMusic.SetEnable(enable);

            _playerDatabase.HasEnableMusic = enable;
            MuteAudio();
        }

        public void SetVolumeSoundFromSlider()
        {
            _playerDatabase.VolumeSound = _soundSlider.GetActiveCount();
        }

        public void SetVolumeMusicFromSlider()
        {
            _playerDatabase.VolumeMusic = _musicSlider.GetActiveCount();
        }

        public void SetVolumeSound()
        {
            foreach (var sound in sounds)
            {
                sound.volume = _playerDatabase.VolumeSound * modifier;
            }
        }

        public void SetVolumeMusic()
        {
            foreach (var music in musics)
            {
                music.volume = _playerDatabase.VolumeMusic * modifier;
            }
        }
    }
}