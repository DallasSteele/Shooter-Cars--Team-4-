using UnityEngine;

namespace ShooterCar.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float m_Limit;
        [SerializeField] private float m_MoveSpeed;

        private Vector3 m_TargetPosition;

        private void Start()
        {
            SetRandomTargetPosition();
        }

        private void Update()
        {
            MovementLoop();
        }

        private void MovementLoop()
        {
            transform.position = Vector3.Lerp(transform.position, m_TargetPosition, m_MoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_TargetPosition) < 0.1f)
            {
                SetRandomTargetPosition();
            }
        }

        private void SetRandomTargetPosition()
        {
            float randomX = Random.Range(-m_Limit, m_Limit);
            m_TargetPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        }
    }
}
