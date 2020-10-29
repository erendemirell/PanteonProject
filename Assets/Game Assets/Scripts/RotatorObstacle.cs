using UnityEngine;

public class RotatorObstacle : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
