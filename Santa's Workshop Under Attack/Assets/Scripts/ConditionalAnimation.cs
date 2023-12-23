using UnityEngine;

public abstract class ConditionalAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public abstract bool Condition();
    private void Update()
    {
        anim.SetBool("condition", Condition());
    }
}
