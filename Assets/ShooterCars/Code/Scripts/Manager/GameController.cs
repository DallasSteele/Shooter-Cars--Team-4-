using UnityEngine;

using ShooterCar.SO;

namespace ShooterCar.Manager
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private GameObject m_PlayerObject;

        public delegate void GameAction();
        public GameAction OnGameStart { get; set; }
        public GameAction OnGameOver { get; set; }
        public GameAction OnEnemyDestroy { get; set; }

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