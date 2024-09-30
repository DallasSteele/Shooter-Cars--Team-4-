using UnityEngine;

namespace ShooterCar.Manager
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private GameObject m_PlayerObject;

        public delegate void GameAction();
        public GameAction OnGameStart { get; set; } = delegate { };
        public GameAction OnGameOver { get; set; } = delegate { };
        public GameAction OnGameRestart { get; set; } = delegate { };
        public GameAction OnBossSpawn { get; set; } = delegate { };
        public GameAction OnBossDefeated { get; set; } = delegate { };
        public GameAction OnEnemyDestroy { get; set; } = delegate { };
        public GameAction OnRoadLoop { get; set; } = delegate { };
        public GameAction OnHit {  get; set; } = delegate { };

        public static GameController Instance { get; private set; }
        public Camera MainCamera { get { return m_Camera; } }
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