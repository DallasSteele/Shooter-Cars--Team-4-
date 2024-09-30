using UnityEngine;

namespace ShooterCar.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_SFXSource, m_BGMSource;
        [SerializeField] private AudioClip m_PlayerFireSFX, gameplayBGM, bossBGM, victoryBGM;

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            GameController.Instance.OnGameStart += GameplayBGM;
            GameController.Instance.OnBossSpawn += BossBGM;
            GameController.Instance.OnBossDefeated += VictoryBGM;
            GameController.Instance.OnGameRestart += GameplayBGM;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameStart -= GameplayBGM;
            GameController.Instance.OnBossSpawn -= BossBGM;
            GameController.Instance.OnBossDefeated -= VictoryBGM;
            GameController.Instance.OnGameRestart -= GameplayBGM;
        }

        public void PlaySFX(AudioClip clip)
        {
            m_SFXSource.PlayOneShot(clip);
        }

        private void GameplayBGM()
        {
            PlayBGM(gameplayBGM);
        }

        private void BossBGM()
        {
            PlayBGM(bossBGM);
        }

        private void VictoryBGM()
        {
            PlayBGM(victoryBGM);
        }

        private void PlayBGM(AudioClip clip)
        {
            Debug.Log("gg");
            m_BGMSource.clip = clip;
            m_BGMSource.Play();
        }
    }
}