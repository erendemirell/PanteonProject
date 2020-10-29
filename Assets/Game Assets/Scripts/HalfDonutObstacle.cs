using UnityEngine;

public class HalfDonutObstacle : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Vector3[] target;

    int currentTarget = 0;
    Transform stick;

    // Start is called before the first frame update
    void Start()
    {
        stick = transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stick.transform.localPosition = Vector3.MoveTowards(stick.transform.localPosition, target[currentTarget], speed * Time.deltaTime);

        if (stick.transform.localPosition == target[currentTarget])
        {
            currentTarget = (currentTarget + 1) % target.Length;
        }
    }
}
