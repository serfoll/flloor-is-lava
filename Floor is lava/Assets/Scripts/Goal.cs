using UnityEngine;

public class Goal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.isLevelCompleted = true;
            GameController.GameOver();
        }
    }
}
