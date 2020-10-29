using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    int currentPoint;

    void Start()
    {
        transform.position = patrolPoints[0].position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotateSpeed);

        if (transform.position == patrolPoints[currentPoint].position)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}
