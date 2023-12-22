using UnityEngine;

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

    public void LoadTemplate(Template temp)
    {
        gen.Clear();
        foreach (var tile in temp.tiles)
        {
            gen.vox[tile.x][tile.y][tile.z] = true;
        }
        gen.Generate();
    }
    public void NextTemplate()
    {
        Template next = templates[Random.Range(0, templates.Length)];
        LoadTemplate(next);
    }
}