using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "New User Button", menuName = "Interface/User/Button")]
public class InterfaceButton : InterfaceOption
{
    [SerializeField] private InterfaceAction interfaceAction;
    [SerializeField] private string buttonText;
    public override void Create(Transform parent)
    {
        GameObject gameInstance = instance as GameObject;
        Button inst = gameInstance.GetComponent<Button>();

        TextMeshProUGUI textMesh = gameInstance.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = buttonText;

        Button newInstance = Instantiate(inst, parent);

        if (interfaceAction == null) return;
        newInstance.onClick.AddListener(interfaceAction.Do);
    }
}
