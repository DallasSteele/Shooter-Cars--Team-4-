using ShooterCar.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ShooterCar.Utilities
{
    public class HitFeedback : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume volume;

        private void OnEnable()
        {
            GameController.Instance.OnHit += Apply;
        }

        private void OnDisable()
        {
            GameController.Instance.OnHit -= Apply;
        }

        private void Apply()
        {
            StartCoroutine(ApplyHitFeedback());
        }

        private IEnumerator ApplyHitFeedback()
        {
            volume.weight = 1.0f;
            while (volume.weight > 0)
            {
                volume.weight -= .1f;
                yield return new WaitForSeconds(.1f);
            }

            yield break;
        }
    }
}