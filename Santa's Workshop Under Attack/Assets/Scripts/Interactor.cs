using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Interactable
{
    public string name;
    public UnityEvent onOpen;
    public UnityEvent onClose;
}
public class Interactor : MonoBehaviour
{
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private LayerMask validLayers;
    [SerializeField] private float rayDist = 4.0f;
    [SerializeField] private string interact;
    [SerializeField] private UnityEvent onLookingAt;
    [SerializeField] private UnityEvent onLookingAway;
    [SerializeField] private UnityEvent onOpenAny;
    [SerializeField] private UnityEvent onCloseAny;
    private Interactable openTab;
    private bool wasLookingAt;

    private void LookingAt()
    {
        if (!wasLookingAt) {
            onLookingAt.Invoke();
            wasLookingAt = true;
        }
    }
    private void LookingAway()
    {
        if (wasLookingAt)
        {
            onLookingAway.Invoke();
            wasLookingAt = false;
        }
    }
    private void Update()
    {
        if (openTab != null)
        {
            if (!Input.GetButtonDown(interact)) return;

            openTab.onClose.Invoke();
            onCloseAny.Invoke();

            openTab = null;
            return;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (!Physics.Raycast(ray, out hit, rayDist, validLayers))
        {
            LookingAway();
            return;
        }
        bool found = false;
        foreach (var interactable in interactables)
        {
            if (!hit.transform.gameObject.CompareTag(interactable.name)) continue;

            LookingAt();
            if (!Input.GetButtonDown(interact)) return;

            interactable.onOpen.Invoke();
            onOpenAny.Invoke();

            openTab = interactable;
            found = true;
            LookingAway();
        }
        if (!found) LookingAway();
    }
}
