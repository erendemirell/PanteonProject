using UnityEngine;

public class FinishLine : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            gameManager.FinishGame();
        }

        if (collision.transform.tag == "Opponent")
        {
            gameManager.OpponentFinish(collision);
        }
    }
}
