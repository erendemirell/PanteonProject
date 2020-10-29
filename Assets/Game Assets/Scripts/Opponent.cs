using System.Collections;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    [SerializeField] float rayLenght;
    [SerializeField] float lookForwardTime;
    [SerializeField] Transform opponentParent;

    float moveSpeed;
    bool isRayHitSomething;
    bool isMoving;
    bool isDead;

    Vector3 offset = new Vector3(0, 1, 0);
    Quaternion startingAngle = Quaternion.Euler(0, -45, 0);
    Quaternion increaseAngle = Quaternion.Euler(0, 5, 0);
    Vector3 lookDirection = Vector3.forward;
    Vector3 startPosition;

    public CharacterState opponentState;
    CharacterAnimation characterAnimation;
    GameManager gameManager;

    private void Awake()
    {
        characterAnimation = new CharacterAnimation(GetComponent<Animator>());
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(2f, 4f);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        CheckXPosition();
        CheckCharacterIsGrounded();
    }

    private void FixedUpdate()
    {
        if (opponentState == CharacterState.Moving && !isDead && gameManager.startGame)
        {
            Move();
            isMoving = true;
            characterAnimation.MoveAnimation(isMoving);
        }
        else
        {
            StopAnimation();
        }
    }

    private void StopAnimation()
    {
        isMoving = false;
        characterAnimation.MoveAnimation(isMoving);
    }

    private void CheckCharacterIsGrounded()
    {
        if (transform.position.y < -8)
        {
            Die();
        }
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
        isDead = true;
        characterAnimation.DeadAnimation(isDead);
        StartCoroutine(SpawnRestart());
    }

    IEnumerator SpawnRestart()
    {
        yield return new WaitForSeconds(4.5f);

        Instantiate(this.gameObject, startPosition, Quaternion.identity).transform.parent = opponentParent;
        Destroy(this.gameObject);
    }

    private void Move()
    {
        transform.Translate(lookDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void SetDirection()
    {
        if (transform.position.z >= 50 && transform.position.z <= 130)
        {
            if (transform.position.x > 0.5)
            {
                FixLookDirection(-45);
            }
            else if (transform.position.x < -0.5)
            {
                FixLookDirection(45);
            }
            else
            {
                FixLookDirection(0);
            }

            return;
        }

        RaycastHit raycastHit;
        Quaternion angle = transform.rotation * startingAngle;
        Vector3 direction = angle * Vector3.forward;

        for (int i = 0; i < 19; i++)
        {
            if (Physics.Raycast(transform.position + offset, direction, out raycastHit, rayLenght))
            {
                if (raycastHit.collider.tag == "Obstacle" || raycastHit.collider.name == "RotatingStick")
                {
                    isRayHitSomething = true;

                    if (i < 9)
                    {
                        ChangeLookDirection(true);
                    }
                    else
                    {
                        ChangeLookDirection(false);
                    }
                }
            }
            else
            {
                isRayHitSomething = false;
            }

            direction = increaseAngle * direction;
        }

        if (!isRayHitSomething)
        {
            FixLookDirection(0);
        }
    }

    void ChangeLookDirection(bool turnRight)
    {
        if (turnRight)
        {
            transform.Rotate(Vector3.up, 5);
        }
        else
        {
            transform.Rotate(-Vector3.up, 5);
        }
    }

    private void FixLookDirection(float yAngle)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), lookForwardTime * Time.deltaTime);
    }

    void CheckXPosition()
    {
        if (transform.position.x > 6)
        {
            FixLookDirection(-45);
        }
        else if (transform.position.x < -6)
        {
            FixLookDirection(45);
        }
    }
}
