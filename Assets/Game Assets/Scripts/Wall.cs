using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Wall Values")]
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float moveSpeed;

    [Header("Paint Object")]
    [SerializeField] GameObject paintPercent;

    public void BringTheWall()
    {
        StartCoroutine(BringTheWallCoroutine());
    }

    IEnumerator BringTheWallCoroutine()
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            yield return null;
        }

        paintPercent.SetActive(true);
    }
}
