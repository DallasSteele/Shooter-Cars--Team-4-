using System.Collections;

using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Enemy
{
    public class DisableBoss : MonoBehaviour
    {
        private void OnEnable()
        {
            GameController.Instance.OnGameRestart += Deactivate;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameRestart -= Deactivate;
        }

        private void Deactivate()
        {
            StartCoroutine(Disable());
        }

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(5);

            gameObject.SetActive(false);
        }
    }
}