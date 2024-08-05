using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Utilities
{
    public class Projectile : MonoBehaviour
    {
        //#serialization
        [SerializeField] private TrailRenderer m_Trail;
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float m_BulletSpeed;

        private Transform m_Muzzle;
        private float m_LongLife;
        private string IgnoreObject = "";

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
            if (collision.collider.CompareTag(IgnoreObject)) return;
            ReturnBullet("Kena " + collision.collider.name);
            if(collision.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(1);
            }
        }

        public void Shoot(Transform muzzle, Vector3 target, string ignoreObject)
        {
            m_Muzzle = muzzle;
            transform.position = muzzle.position;
            transform.LookAt(target);
            IgnoreObject = ignoreObject;
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
