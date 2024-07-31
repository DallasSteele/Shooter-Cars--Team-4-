using System.Collections;

using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Utilities
{
    public class Projectile : MonoBehaviour
    {
        //#serialization
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private float damage = 10f;

        public GameObject IgnoreObject { get; set; }
        public Transform Muzzle { get; set; }
        public RaycastHit Hit { get; set; }
        public Vector3 Offset;
        public Vector3 Direction { get; set; }

        private void Start()
        {
            StartCoroutine(LifeTime());
        }

        private void Update()
        {
            if (Muzzle == null) return;
            transform.position += transform.forward * 10 * Time.deltaTime;
            //transform.Translate(Direction * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(Muzzle.position, Hit.point, 50 * Time.deltaTime);
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(lifetime);
            ReturnBullet("dah lama cuy");
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if (IgnoreObject != null && collision.gameObject == IgnoreObject) return;
            ReturnBullet("Kena " + collision.collider.name);
            if(collision.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(1);
            }
        }

        private void ReturnBullet(string x = null)
        {
            if (x != null) Debug.LogWarning(x);
            ObjectPooling.Instance.ReturnBullet(gameObject);
        }
    }
}
