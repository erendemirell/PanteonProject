using System.Collections;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField] Transform opponentsParent;

    public int playerPosition;
    public bool isGameFinished;
    int playerPastOpponentCount;
    int totalCharacterCount = 11;

    Opponent[] opponents;
    Player player;
    UIManager uiManager;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rank());
    }

    // Update is called once per frame
    IEnumerator Rank()
    {
        playerPastOpponentCount = 0;

        opponents = new Opponent[10];
        opponents = opponentsParent.GetComponentsInChildren<Opponent>();

        foreach (var item in opponents)
        {
            bool isPlayerAhead = player.transform.position.z > item.gameObject.transform.position.z;

            if (isPlayerAhead)
            {
                playerPastOpponentCount++;
            }
        }

        if (!isGameFinished)
        {
            playerPosition = totalCharacterCount - playerPastOpponentCount;
        }

        WritePlayerRank(playerPosition);

        yield return new WaitForFixedUpdate();

        StartCoroutine(Rank());

    }

    private void WritePlayerRank(int position)
    {
        uiManager.rankText.text = "Rank\n" + position.ToString() + "/11";
    }
}
