using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    public FloatingJoystick floatingJoystick;

    bool isMoving;
    bool isDead;

    public CharacterState playerState;
    CharacterAnimation characterAnimation;
    UIManager uiManager;
    GameManager gameManager;

    private void Awake()
    {
        characterAnimation = new CharacterAnimation(GetComponent<Animator>());
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckPlayerIsGrounded();
    }

    private void CheckPlayerIsGrounded()
    {
        if (transform.position.y < -8)
        {
            Die();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && (playerState == CharacterState.Moving) && !isDead && gameManager.startGame)
        {
            Move();
        }
        else
        {
            StopAnimation();
        }
    }

    private void Move()
    {
        isMoving = true;
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            characterAnimation.MoveAnimation(isMoving);
        }
        transform.position += (direction * moveSpeed * Time.deltaTime);
    }

    private void StopAnimation()
    {
        isMoving = false;
        characterAnimation.MoveAnimation(isMoving);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            Die();
        }
    }

    private void Die()
    {
        floatingJoystick.gameObject.SetActive(false);
        isDead = true;
        characterAnimation.DeadAnimation(isDead);
        StartCoroutine(WaitDeadAnimation());
    }

    IEnumerator WaitDeadAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        uiManager.gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
