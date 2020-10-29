using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] bool isClockwise;
    [SerializeField] float turnSpeed;

    Vector3 axis;
    Rigidbody rigidbody;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        axis.z += turnSpeed * Time.fixedDeltaTime;

        if (isClockwise)
        {
            rigidbody.MoveRotation(Quaternion.Euler(-axis));
        }
        else
        {
            rigidbody.MoveRotation(Quaternion.Euler(axis));
        }
    }
}
