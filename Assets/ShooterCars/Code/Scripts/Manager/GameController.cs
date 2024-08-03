using UnityEngine;

namespace ShooterCar.Manager
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Camera m_MainCam;
        [SerializeField] private GameObject m_PlayerObject;
        [SerializeField] private Vector3 m_Offset;

        public delegate void Action();
        public Action OnGameStart { get; set; }
        public Action OnGameOver { get; set; }
        public Action OnEnemyDestroy { get; set; }

        public static GameController Instance { get; private set; }
        public Camera MainCamera { get { return m_MainCam; } }
        public GameObject Player { get { return m_PlayerObject; } }
        public bool HoverButton { get; set; }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}