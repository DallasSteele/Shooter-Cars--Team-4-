using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using ShooterCar.Manager;

public class carMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sideStepSpeed = 3f;
    public float sideStepDistance = 2f;
    public float leftBoundary = -3f;
    public float rightBoundary = 3f;
    public float inputCooldown = 0.5f;

    public Button leftButton;
    public Button rightButton;

    private float currentXPosition;
    private float lastInputTime = -1f;
    private gameManager gameManager;
    private Transform m_Player { get { return GameController.Instance.Player.transform; } }


    void Start()
    {
        currentXPosition = 0f;

        // Add listeners to the buttons
        leftButton.onClick.AddListener(SideStepRight);
        rightButton.onClick.AddListener(SideStepLeft);
        //Call the game manager
        gameManager = FindObjectOfType<gameManager>();
    }

    void Update()
    {
        //Player input handling code here
        if (gameManager.isGameActive)
        {
            // Move the car forward automatically
            //transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void SideStepLeft()
    {
        if (Time.time - lastInputTime > inputCooldown)
        {
            float targetXPosition = currentXPosition - sideStepDistance;
            if (targetXPosition >= leftBoundary)
            {
                lastInputTime = Time.time;
                StartCoroutine(SideStep(targetXPosition));
            }
        }
    }

    private void SideStepRight()
    {
        if (Time.time - lastInputTime > inputCooldown)
        {
            float targetXPosition = currentXPosition + sideStepDistance;
            if (targetXPosition <= rightBoundary)
            {
                lastInputTime = Time.time;
                StartCoroutine(SideStep(targetXPosition));
            }
        }
    }

    private System.Collections.IEnumerator SideStep(float targetXPosition)
    {
        float elapsedTime = 0f;
        float startPosition = currentXPosition;

        while (elapsedTime < 1f)
        {
            float interpolation = SmoothStep(0f, 1f, elapsedTime / 1f);
            currentXPosition = Mathf.Lerp(startPosition, targetXPosition, interpolation);
            m_Player.position = new Vector3(currentXPosition, m_Player.position.y, m_Player.position.z);
            elapsedTime += Time.deltaTime * sideStepSpeed;
            yield return null;
        }

        currentXPosition = targetXPosition;
        m_Player.position = new Vector3(currentXPosition, m_Player.position.y, m_Player.position.z);
    }

    private float SmoothStep(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        return start + (end - start) * (value * value * (3f - 2f * value));
    }
}