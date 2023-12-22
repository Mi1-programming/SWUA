using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class Template
{
    public string name;
    public Vector3Int[] tiles;
}
public class TemplateSystem : MonoBehaviour
{
    public Template[] templates;
    [SerializeField] private Generation gen;
    [SerializeField] private MeshRenderer modelerTarget;
    [SerializeField] private Material templateMaterial;
    [SerializeField] private UnityEvent onDisplay;
    [SerializeField] private UnityEvent onHide;
    [HideInInspector] public Template currentTemplate;

    private Material originMaterial;
    private Coroutine showTemplateCoroutine;

    public bool[][][] TemplateToBuild(Template temp)
    {
        bool[][][] newBuild = MuchClean.Create3DMatrix(gen.size);
        foreach (var coord in temp.tiles)
        {
            newBuild[coord.x][coord.y][coord.z] = true;
        }
        return newBuild;
    }
    public float CalculateCondition(Template compare)
    {
        bool[][][] tempBuild = TemplateToBuild(compare);
        float right = 0;

        int x = 0;
        for (x = 0; x < gen.size; x++)
        {
            int y = 0;
            for (y = 0; y < gen.size; y++)
            {
                int z = 0;
                for (z = 0; z < gen.size; z++)
                {
                    right += gen.vox[x][y][z] && true == tempBuild[x][y][z] ? 1.0f : 0.0f;
                    right -= gen.vox[x][y][z] && false == tempBuild[x][y][z] ? 1.0f : 0.0f;
                }
            }
        }
        return Mathf.Max(right, 0) / compare.tiles.Length;
    }
    private void NextTemplate()
    {
        Template next = templates[Random.Range(0, templates.Length)];
        LoadTemplate(next);
        currentTemplate = next;
    }
    private IEnumerator ShowTemplateIEnu(float time)
    {
        onDisplay.Invoke();
        NextTemplate();
        modelerTarget.sharedMaterial = templateMaterial;

        yield return new WaitForSecondsRealtime(time);

        onHide.Invoke();
        modelerTarget.sharedMaterial = originMaterial;
    }
    public void ShowTemplate(float time)
    {
        if (showTemplateCoroutine != null)
        {
            StopCoroutine(showTemplateCoroutine);
            modelerTarget.sharedMaterial = originMaterial;
        }
        originMaterial = modelerTarget.sharedMaterial;
        showTemplateCoroutine = StartCoroutine(ShowTemplateIEnu(time));
    }
    public void LoadTemplate(Template temp)
    {
        gen.vox = TemplateToBuild(temp);
        gen.Generate();
    }
}