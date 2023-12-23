using UnityEngine;

[CreateAssetMenu(fileName = "New Exit Action", menuName = "Interface/Actions/Exit")]
public class ExitAction : InterfaceAction
{
    public override void Do()
    {
        Debug.Log("We have quit!");
        Application.Quit();
    }
}
