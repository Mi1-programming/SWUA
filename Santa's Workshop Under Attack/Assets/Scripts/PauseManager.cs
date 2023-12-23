using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private string pause;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onResume;

    public static PauseManager self;
    public static bool paused;

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        onPause.Invoke();
    }
    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        onResume.Invoke();
    }
    private void Awake()
    {
        self = this;
    }
    private void Update()
    {
        if (Interactor.openTab != null) return;
        if (!Input.GetButtonDown(pause)) return;
        if (paused) Resume(); else Pause();
    }
}
