using UnityEngine;

namespace HW.Boot
{
    public class GameManager : MonoBehaviour
    {
        public delegate void Action();
        public Action OnGameStart;
        public Action OnGameOver;
        public Action OnEnemyDestroy;

        public static GameManager Instance { get; private set; }

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