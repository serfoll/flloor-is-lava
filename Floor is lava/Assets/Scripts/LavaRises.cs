using UnityEngine;

public class LavaRises : MonoBehaviour
{
    public float riseSpeed;
    Vector3 lavaPos;

    private void Update()
    {
        if (!GameController.gameIsRunning) return;

        LavaRise();
    }

    void LavaRise()
    {
        lavaPos.y += riseSpeed * Time.deltaTime;

        this.transform.Translate(Vector3.up * lavaPos.y * 0.01f, Space.World);
    }
   
}
