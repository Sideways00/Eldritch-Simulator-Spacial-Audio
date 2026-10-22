using UnityEngine;

public class Roadkill : MonoBehaviour
{
    public int points = 10;

    [Header("Crash Sound")]
    public AudioClip crashSound;
    [Range(0f, 1f)]
    public float crashVolume = 1f;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (other.GetComponentInParent<CarMovement>() != null)
        {
            collected = true;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(points);
            }

            if (crashSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    crashSound,
                    transform.position,
                    crashVolume
                );
            }

            Destroy(gameObject);
        }
    }
}

