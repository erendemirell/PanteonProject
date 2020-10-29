using System.Collections;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    [SerializeField] float pushForce = 10f;
    [SerializeField] int forceCount = 10;

    float waitTime;

    Player player;
    Opponent opponent;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        opponent = FindObjectOfType<Opponent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            player.playerState = CharacterState.Pushing;
            StartCoroutine(ApplyForce(collision));
        }

        if (collision.transform.tag == "Opponent")
        {
            opponent.opponentState = CharacterState.Pushing;
            StartCoroutine(ApplyForce(collision));
        }
    }

    IEnumerator ApplyForce(Collision other)
    {
        Vector3 force = other.contacts[0].point - other.transform.position;
        force.y = 0;
        force.Normalize();

        int i = 0;
        while (i < forceCount)
        {
            other.transform.GetComponent<Rigidbody>().AddForce(-force * pushForce);
            i++;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);//little delay after hit

        if (other.collider.GetComponent<Player>() != null)
        {
            player.playerState = CharacterState.Moving;
        }
        else
        {
            opponent.opponentState = CharacterState.Moving;
        }
    }
}
