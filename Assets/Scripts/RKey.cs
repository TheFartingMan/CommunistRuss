using UnityEngine;

public class RKey : MonoBehaviour
{
    public GameOver GameOverScript;
    void Update()
    {
        if (Input.GetKeyDown("r") == true)
        {
            GameOverScript.RestartGame();
        }
    }
}
