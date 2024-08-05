using UnityEngine;

namespace ShooterCar.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_SFXSource;
        [SerializeField] private AudioClip m_PlayerFireSFX;

        private void OnEnable()
        {
            GameController.Instance.OnFire += PlayFireSFX;
        }

        private void OnDisable()
        {
            GameController.Instance.OnFire -= PlayFireSFX;
        }

        private void PlayFireSFX()
        {
            m_SFXSource.PlayOneShot(m_PlayerFireSFX);
        }
    }
}