using UnityEngine;
using UnityEngine.UI;

public abstract class ConditionalButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public abstract bool Condition();
    private void Update()
    {
        button.interactable = Condition();
    }
}
