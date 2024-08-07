using UnityEngine;

namespace ShooterCar.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_SFXSource;
        [SerializeField] private AudioClip m_PlayerFireSFX;

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

        public void PlaySFX(AudioClip clip)
        {
            m_SFXSource.PlayOneShot(clip);
        }
    }
}