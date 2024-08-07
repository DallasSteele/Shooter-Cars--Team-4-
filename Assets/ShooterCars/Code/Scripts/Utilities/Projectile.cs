using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Utilities
{
    public class Projectile : MonoBehaviour
    {
        //#serialization
        [SerializeField] private TrailRenderer m_Trail;
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private float m_BulletSpeed;

        private float m_LongLife, m_DamageAmount;
        private string m_IgnoreObject = "";

        private void Update()
        {
            if (m_LongLife < lifetime)
            {
                m_LongLife += Time.deltaTime;
            }
            else
            {
                ReturnBullet();
            }

            transform.position += m_BulletSpeed * Time.deltaTime * transform.forward;
            //transform.Translate(-transform.forward * m_BulletSpeed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(Muzzle.position, Hit.point, 50 * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(m_IgnoreObject)) return;
            ReturnBullet("Kena " + collision.collider.name);
            if(collision.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_DamageAmount);
            }
        }

        public void Initialize(float damageAmount, float bulletSpeed)
        {
            m_DamageAmount = damageAmount;
            m_BulletSpeed = bulletSpeed;
        }

        public void Shoot(Transform muzzle, Vector3 target, string ignoreObject)
        {
            transform.position = muzzle.position;
            transform.LookAt(target);
            m_IgnoreObject = ignoreObject;
        }

        private void ReturnBullet(string x = null)
        {
            if (x != null) Debug.LogWarning(x);

            m_LongLife = 0;
            m_Trail.Clear();
            ObjectPooling.Instance.ReturnBullet(gameObject);
        }
    }
}
