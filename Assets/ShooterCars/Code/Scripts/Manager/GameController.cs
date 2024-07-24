using UnityEngine;

namespace ShooterCar.Manager
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Camera m_MainCam;

        public delegate void Action();
        public Action OnGameStart { get; set; }
        public Action OnGameOver { get; set; }
        public Action OnEnemyDestroy { get; set; }

        public static GameController Instance { get; private set; }
        public Camera MainCamera { get { return m_MainCam; } }

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