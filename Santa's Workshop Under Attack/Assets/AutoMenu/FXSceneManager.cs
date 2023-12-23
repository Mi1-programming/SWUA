using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FXSceneManager : MonoBehaviour
{
    // FOR DEBUG PURPOSES
    [SerializeField] private Transition[] debugTargets;
    public static Transition[] _debugTargets;
    public static string debugTarget = "0";

    // VARIABLES
    public static FXSceneManager singelton;
    public static Transition current;
    public static bool inProgress = false;

    private void Awake()
    {
        singelton = this;
    }
    private void Start()
    {
        // If debug targets are found. Make them static so they retain across scenes
        if (debugTargets.Length > 0)
            _debugTargets = debugTargets;

        // If coming from a transition apply the ending
        if (current != null)
            StartCoroutine(FXSceneLoadedIE(current));
    }
    /// <summary>
    /// Sets the next transition
    /// </summary>
    /// <param name="fx">Transition effect scriptable object</param>
    public void SetFX(Transition fx)
    {
        current = fx;
    }
    /// <summary>
    /// Loads a scene while using a transition
    /// </summary>
    /// <param name="sceneBuildIndex">Scene index</param>
    public void FXLoadScene(int sceneBuildIndex)
    {
        StartCoroutine(FXLoadSceneIE(sceneBuildIndex, current));
    }
    /// <summary>
    /// Loads the next scene using a transition
    /// </summary>
    public void FXLoadNextScene()
    {
        StartCoroutine(FXLoadSceneIE((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings, current));
    }

    // The IEnumerators the previous methods call
    public static IEnumerator FXLoadSceneIE(int sceneBuildIndex, Transition fx)
    {
        if (inProgress) yield break;
        inProgress = true;

        yield return fx.OnStartTransition();

        SceneManager.LoadSceneAsync(sceneBuildIndex);
    }
    public static IEnumerator FXSceneLoadedIE(Transition fx)
    {
        yield return fx.OnEndTransition();

        current = null;
        inProgress = false;
    }
    // Renders the transition
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (current == null)
        {
            Graphics.Blit(source, destination);
            return;
        }
        current.Apply(source, destination);
    }
    // Renders the debug GUI
    private void OnGUI()
    {
        if (_debugTargets == null) return;

        GUI.Label(new Rect(10, 10, 300, 60), "TRANSITIONAL EFFECT SCENE TRANSITIONER");
        GUI.Label(new Rect(10, 30, 150, 60), "Scene: " + SceneManager.GetActiveScene().buildIndex);
        GUI.Label(new Rect(10, 50, 150, 60), "FPS: " + 1f / Time.deltaTime);
        GUI.Label(new Rect(10, 70, 150, 60), "Processing: " + ((current != null) ? current.name : "None"));

        debugTarget = GUI.TextField(new Rect(10, 120, 150, 20), debugTarget);
        int intIndex;
        if (int.TryParse(debugTarget, out intIndex) == false) return;

        int resultIndex = Mathf.Clamp(intIndex, 0, _debugTargets.Length - 1);
        GUI.Label(new Rect(10, 90, 150, 60), "Target: " + _debugTargets[resultIndex].name);

        if (GUI.Button(new Rect(10, 150, 150, 100), "Transition"))
        {
            SetFX(_debugTargets[resultIndex]);
            FXLoadNextScene();
        } 
    }
}