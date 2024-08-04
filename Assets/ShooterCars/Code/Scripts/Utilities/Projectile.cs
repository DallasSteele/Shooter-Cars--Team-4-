using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Utilities
{
    public class Projectile : MonoBehaviour
    {
        //#serialization
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float m_BulletSpeed;
        [SerializeField] private TrailRenderer m_Trail;

        private float m_LongLife;

        public string IgnoreObject { get; set; } = "";
        public Transform Muzzle { get; set; }

        private void Update()
        {
            if(m_LongLife < lifetime)
            {
                m_LongLife += Time.deltaTime;
            }
            else
            {
                ReturnBullet();
            }

            if (Muzzle == null) return;
            transform.position += transform.forward * m_BulletSpeed * Time.deltaTime;
            //transform.Translate(Direction * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(Muzzle.position, Hit.point, 50 * Time.deltaTime);
        }

        private void OnEnable()
        {
            m_Trail.Clear();
            m_Trail.time = .5f;
            m_Trail.emitting = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(IgnoreObject)) return;
            ReturnBullet("Kena " + collision.collider.name);
            if(collision.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(1);
            }
        }

        private void ReturnBullet(string x = null)
        {
            if (x != null) Debug.LogWarning(x);

            m_LongLife = 0;
            m_Trail.time = 0;
            m_Trail.emitting = false;
            m_Trail.Clear();
            ObjectPooling.Instance.ReturnBullet(gameObject);
        }
    }
}
