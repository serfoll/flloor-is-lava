using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public Canvas levelCompleteCanvas;
    public AudioManager audio;

    private void Start()
    {
        GameController.gameOverCanvas = gameOverCanvas;
        GameController.levelCompleteCanvas = levelCompleteCanvas;
        GameController.audio = audio;
    }

}
