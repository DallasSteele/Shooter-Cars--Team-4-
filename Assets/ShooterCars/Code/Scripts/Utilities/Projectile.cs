using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Parent;

namespace ShooterCar.Utilities
{
    public class Projectile : MonoBehaviour
    {
        //#serialization
        [SerializeField] private TrailRenderer m_Trail;

        [SerializeField] private float lifetime = 5f;

        private EnumStore.Bullet m_BulletType;

        private float m_LongLife, m_DamageAmount, m_BulletSpeed;

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

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag(m_IgnoreObject)) return;

            switch (m_BulletType)
            {
                case EnumStore.Bullet.Standard:
                    StandardType(collision);
                    break;
                case EnumStore.Bullet.Explode:
                    ExplodeType(transform.position);
                    break;
                default:
                    break;
            }

            ReturnBullet("Kena " + collision.name);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 5);
        }

        public void Initialize(EnumStore.Bullet bulletType, float damageAmount, float bulletSpeed)
        {
            m_BulletType = bulletType;
            m_DamageAmount = damageAmount;
            m_BulletSpeed = bulletSpeed;
        }

        public void Shoot(Transform muzzle, Vector3 target, string ignoreObject)
        {
            transform.position = muzzle.position;
            transform.LookAt(target);
            m_IgnoreObject = ignoreObject;
        }

        private void StandardType(Collider collision)
        {
            if (collision.TryGetComponent<HealthSystem>(out var damageable))
            {
                damageable.TakeDamage(m_DamageAmount);
            }
        }

        private void ExplodeType(Vector3 position)
        {
            ObjectPooling.Instance.GetExplodeEffect().transform.position = position;

            //int maxItem = 5;
            //Collider[] collisions = new Collider[maxItem];
            //int itemCount = Physics.OverlapSphereNonAlloc(position, 5, collisions);
            //for(int i = 0; i < itemCount; i++)
            Collider[] collisions = Physics.OverlapSphere(position, 5);
            foreach(var item in collisions)
            {
                if(item.TryGetComponent<HealthSystem>(out var damageable))
                {
                    damageable.TakeDamage(m_DamageAmount);
                }
            }
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
