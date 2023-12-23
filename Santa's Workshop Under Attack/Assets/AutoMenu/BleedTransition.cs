using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Bleed Transition", menuName = "Transitions/Bleed Transition")]
public class BleedTransition : Transition
{
    [System.NonSerialized] private RenderTexture screenGrab;

    public override IEnumerator OnStartTransition()
    {
        waited = duration;
        yield break;
    }
    public override void SetShaderVariables(RenderTexture source, RenderTexture destination, Material mat)
    {
        base.SetShaderVariables(source, destination, mat);

        if (screenGrab == null)
        {
            screenGrab = new RenderTexture(source);
            Graphics.Blit(source, screenGrab);
        }
        mat.SetTexture("_OGTex", screenGrab);
    }
    public override void Reset()
    {
        base.Reset();
        screenGrab = null;
    }
}
