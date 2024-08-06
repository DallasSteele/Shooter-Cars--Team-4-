using UnityEngine;

using ShooterCar.SO;

namespace ShooterCar.Manager
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlayerObject;
        [SerializeField] private Weapon m_Weapon;

        public delegate void GameAction();
        public GameAction OnGameStart { get; set; }
        public GameAction OnGameOver { get; set; }
        public GameAction OnFire { get; set; }
        public GameAction OnEnemyDestroy { get; set; }

        public static GameController Instance { get; private set; }
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

        public Weapon WeaponData
        {
            get { return m_Weapon; }
            private set
            {
                m_Weapon = value;
            }
        }
    }
}