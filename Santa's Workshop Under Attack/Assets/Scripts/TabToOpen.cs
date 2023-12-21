using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TabToOpen : MonoBehaviour
{
    [SerializeField] private string openKey;
    [SerializeField] private UnityEvent<bool> onTabToggle;
    [SerializeField] private UnityEvent<float> onToggleAnimate;
    [SerializeField] private float toggleTime = 0.25f;
    [SerializeField] private AnimationCurve curve;
    private float toggleProgress = 0.0f;
    private bool active = false;
    private Coroutine lastAnim;

    private void Update()
    {
        if (Input.GetButtonDown(openKey)) {
            active = !active;
            onTabToggle.Invoke(active);

            if (lastAnim != null) StopCoroutine(lastAnim);
            lastAnim = StartCoroutine(Animate());
        }        
    }
    private IEnumerator Animate()
    {
        toggleProgress = 0.0f;
        while (toggleProgress <= toggleTime)
        {
            toggleProgress += Time.deltaTime;
            onToggleAnimate.Invoke(curve.Evaluate(active ? toggleProgress / toggleTime : 1.0f - toggleProgress / toggleTime));
            yield return 0;
        }
    }
}
