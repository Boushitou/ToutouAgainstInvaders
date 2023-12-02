using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioClip[] _songs;
        [SerializeField] private AudioClip[] _shootSounds, _playerHurtSounds, _enemyDeadSounds, _enemyShotSounds, _enemyShootSounds, _bossYelp, _bossDying;

        private Dictionary<string, AudioClip[]> _soundEffects = new Dictionary<string, AudioClip[]>();
        private Dictionary<string, AudioClip> _musics = new Dictionary<string, AudioClip>();

        [SerializeField] private AudioSource _musicSource, _effectSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            DontDestroyOnLoad(gameObject);
            SetupSounds();

            _musicSource.volume = 0.5f;
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                PlayMusic("Menu");
            }
            else
            {
                PlayMusic("Game");
            }
        }

        public void PlayMusic(string musicName, bool isLooped = true)
        {
            if (_musics.ContainsKey(musicName))
            {
                if (_musicSource.clip != _musics[musicName])
                {
                    _musicSource.clip = _musics[musicName];
                    _musicSource.loop = isLooped;
                    _musicSource.Play();
                }
            }
            else
            {
                Debug.LogWarning("No musics has that name !");
            }
        }

        public void PlaySound(string soundName, float volume = 1)
        {
            if (_soundEffects.ContainsKey(soundName))
            {
                int soundIndex = Random.Range(0, _soundEffects[soundName].Length);
                _effectSource.PlayOneShot(_soundEffects[soundName][soundIndex], volume);
            }
            else
            {
                Debug.LogWarning("No sounds has that name !");
            }
        }

        private void SetupSounds()
        {
            _soundEffects["Shoot"] = _shootSounds;
            _soundEffects["Player Hurt"] = _playerHurtSounds;
            _soundEffects["Enemy Dead"] = _enemyDeadSounds;
            _soundEffects["Enemy Shot"] = _enemyShotSounds;
            _soundEffects["Enemy Shoot"] = _enemyShootSounds;
            _soundEffects["Boss Yelp"] = _bossYelp;
            _soundEffects["Boss Dead"] = _bossDying;

            _musics["Menu"] = _songs[0];
            _musics["Game"] = _songs[1];
            _musics["Boss"] = _songs[2];
            _musics["Victory"] = _songs[3];
            _musics["Defeat"] = _songs[4];
        }
    }
}
