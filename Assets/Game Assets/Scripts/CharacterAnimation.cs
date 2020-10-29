using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;

    public CharacterAnimation(Animator animator)
    {
        this.animator = animator;
    }

    public void MoveAnimation(bool isMoving)
    {
        if (isMoving != animator.GetBool("isMoving"))
        {
            animator.SetBool("isMoving", isMoving);
        }
    }

    public void DeadAnimation(bool isDead)
    {
        if (isDead != animator.GetBool("isDead"))
        {
            animator.SetBool("isDead", isDead);
        }
    }
}
