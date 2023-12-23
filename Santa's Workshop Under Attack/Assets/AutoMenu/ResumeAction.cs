using UnityEngine;

[CreateAssetMenu(fileName = "New Resume Action", menuName = "Interface/Actions/Resume")]
public class ResumeAction : InterfaceAction
{
    public override void Do()
    {
        PauseManager.self.Resume();
    }
}