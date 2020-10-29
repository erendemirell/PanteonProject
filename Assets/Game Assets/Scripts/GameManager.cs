using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Vector3 cameraTargetPos;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] GameObject wallPrefab;

    public bool startGame;
    float waitTime = 1f;
    bool stopCoroutine;
    int playerFinishPosition;

    Player player;
    Wall wall;
    Ranking ranking;
    UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
        ranking = FindObjectOfType<Ranking>();
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        startGame = false;

        yield return new WaitForSeconds(1);
        uiManager.startGameText.text = "3";
        yield return new WaitForSeconds(1);
        uiManager.startGameText.text = "2";
        yield return new WaitForSeconds(1);
        uiManager.startGameText.text = "1";
        yield return new WaitForSeconds(1);
        uiManager.startGameText.text = "GO";
        yield return new WaitForSeconds(1);

        startGame = true;
        uiManager.startGameText.gameObject.SetActive(false);
    }

    private void Update()
    {
        IsWallPainted();
    }

    private void IsWallPainted()
    {
        if (uiManager.paintedPercentText.text == "100%\nPainted" && !stopCoroutine)
        {
            stopCoroutine = true;
            StartCoroutine(NextLevelCoroutine());
        }
    }

    IEnumerator NextLevelCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        uiManager.congratulationsText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        uiManager.congratulationsText.gameObject.SetActive(false);

        uiManager.nextLevelPanel.SetActive(true);
    }

    public void FinishGame()
    {
        if (ranking != null)
        {
            ranking.isGameFinished = true;
            playerFinishPosition = ranking.playerPosition;
        }

        StartCoroutine(FinishCorotuine());
    }

    IEnumerator FinishCorotuine()
    {
        yield return new WaitForSeconds(waitTime);

        player.floatingJoystick.gameObject.SetActive(false);

        if (wallPrefab != null)
        {
            wallPrefab.SetActive(true);
            wall = FindObjectOfType<Wall>();
            player.playerState = CharacterState.Painting;
        }

        if (ranking != null)
        {
            uiManager.finishPanel.SetActive(true);
            uiManager.rankText.gameObject.SetActive(false);
            uiManager.aiFinishText.text = "FINISH\nYOUR RANK\n" + playerFinishPosition.ToString();
        }
        else
        {
            StartCoroutine(FinishText());
        }
    }

    IEnumerator FinishText()
    {
        uiManager.finishText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        wall.BringTheWall();
        uiManager.finishText.gameObject.SetActive(false);

        CameraSetting();

        uiManager.paintWallText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        uiManager.paintWallText.gameObject.SetActive(false);

    }

    private void CameraSetting()
    {
        Camera.main.GetComponent<CameraFollow>().enabled = false;

        StartCoroutine(CameraPosition());
    }

    IEnumerator CameraPosition()
    {
        while (Camera.main.transform.position != cameraTargetPos)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraTargetPos, Time.deltaTime * cameraSpeed);

            yield return null;
        }
    }

    public void OpponentFinish(Collision collision)
    {
        StartCoroutine(AIFinishCorotuine(collision));
    }

    IEnumerator AIFinishCorotuine(Collision collision)
    {
        yield return new WaitForSeconds(waitTime);

        collision.collider.GetComponent<Opponent>().opponentState = CharacterState.Finish;
    }
}
