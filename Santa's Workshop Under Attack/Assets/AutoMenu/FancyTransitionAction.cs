using UnityEngine;

[CreateAssetMenu(fileName = "New Fancy Transition Action", menuName = "Interface/Actions/Fancy Transition")]
public class FancyTransitionAction : InterfaceAction
{
    public int scene;
    public Transition effect;
    public override void Do()
    {
        Time.timeScale = 1;
        PauseManager.paused = false;
        FXSceneManager.singelton.SetFX(effect);
        FXSceneManager.singelton.FXLoadScene(scene);
    }
}
