using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Enemy
{
    public class BossHealth : HealthSystem
    {
        [SerializeField] private BossShoot[] bossShoot;
        [SerializeField] private ParticleSystem explodeParticle;
        [SerializeField] private Animator anim;
        [SerializeField] private PlayableAsset spawnCam, destroyCam;

        [SerializeField] private float duration;
        [SerializeField] private float fireInterval = 7;

        private Slider healthBar;

        private bool stopChecking;
        private float currentDuration;

        private void Update()
        {
            CountdownBossTimer();
        }

        private void OnEnable()
        {
            GameController.Instance.OnBossDefeated += ResetBoss;
            GameController.Instance.OnGameRestart += ResetBoss;

            InitialStatus();
        }

        private void OnDisable()
        {
            GameController.Instance.OnBossDefeated -= ResetBoss;
            GameController.Instance.OnGameRestart -= ResetBoss;
        }

        private void CountdownBossTimer()
        {
            if (stopChecking) return;

            if (currentDuration <= duration + fireInterval)
            {
                currentDuration += Time.deltaTime;
            }
            else
            {
                GameController.Instance.OnBossDefeated();
                Animation();
                stopChecking = true;
            }
        }

        private void ResetBoss()
        {
            foreach (var item in bossShoot)
            {
                item.SetFireInterval(fireInterval);
            }
        }

        protected override void InitialStatus()
        {
            currentDuration = 0;
            stopChecking = false;

            if (m_CurrentHealth <= 0)
                MaxHealth();

            SetHealthBar();
            CamCinematic(spawnCam);
        }

        protected override void Die()
        {
            GameController.Instance.OnBossDefeated();
            CamCinematic(destroyCam);
            stopChecking = true;
            explodeParticle.Play();
            StartCoroutine(WaitForDestroy());
            
        }

        private void CamCinematic(PlayableAsset asset)
        {
            if(asset != null)
            {
                GameController.Instance.Director.Play(asset);
            }
        }

        private void Animation()
        {
            if(anim != null)
            {
                anim.enabled = true;
                anim.Play("Escape");
            }
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(2.5f);
            Destroy(gameObject);
        }

        protected override void SetHealthBar()
        {
            healthBar = InterfaceHandle.Instance.bossHealthBar;
            healthBar.maxValue = maxHealth;
            healthBar.value = m_CurrentHealth;
        }
    }
}