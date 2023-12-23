using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private InterfaceOption[] menuOptions;
    private void Start()
    {
        foreach (var option in menuOptions)
        {
            option.Create(transform);
        }
    }
}
