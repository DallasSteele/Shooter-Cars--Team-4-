using UnityEngine;
using UnityEngine.UI;

namespace ShooterCar.Manager
{
    public class InterfaceHandle : MonoBehaviour
    {
        [SerializeField] private Slider bossHealthSlider;

        public Slider bossHealthBar { get { return bossHealthSlider; } }

        public static InterfaceHandle Instance { get; private set; }

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