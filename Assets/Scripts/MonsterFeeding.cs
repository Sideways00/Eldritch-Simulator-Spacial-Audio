using UnityEngine;
using TMPro;

public class MonsterFeeding : MonoBehaviour
{
    [Header("Win/Lose Settings")]
    public int requiredPoints = 100;
    public TMP_Text endingText;

    private bool encounterStarted = false;

    private void Start()
    {
        if (endingText != null)
        {
            endingText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (encounterStarted)
            return;

        CarMovement car = other.GetComponentInParent<CarMovement>();

        if (car == null)
            return;

        encounterStarted = true;

        int score = ScoreManager.Instance != null
            ? ScoreManager.Instance.totalScore
            : 0;

        if (endingText != null)
        {
            endingText.gameObject.SetActive(true);

            if (score >= requiredPoints)
            {
                endingText.text = "YOU WIN!\nThe monster accepts your offering!";
                endingText.color = Color.green;
            }
            else
            {
                endingText.text = "YOU LOSE!\nThe monster rejects your offering!";
                endingText.color = Color.red;
            }
        }
        else
        {
            Debug.LogWarning("Assign the Ending Text in the Inspector!");
        }

        // Stop the car after the ending is triggered.
        car.enabled = false;
    }
}