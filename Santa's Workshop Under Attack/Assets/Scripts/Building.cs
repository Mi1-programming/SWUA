using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Template
{
    public string name;
    public Vector3[] tiles;
}
public class Building : MonoBehaviour
{
    public Generation gen;
    public Template[] templates;

    [SerializeField] private LayerMask buildingLayer;
    [SerializeField] private UnityEvent<Vector3> onPlace;
    [SerializeField] private UnityEvent<Vector3> onBreak;
    [SerializeField] private Camera cam;
    [SerializeField] private string place;
    [SerializeField] private string remove;
    
    public void Clear()
    {
        gen.Clear();
        gen.Generate();
    }
    private Vector3Int RoundVec(Vector3 target)
    {
        return new Vector3Int(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y), Mathf.FloorToInt(target.z));
    }
    private void Update()
    {
        if (Input.GetButtonDown(place) || Input.GetButtonDown(remove))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50.0f, buildingLayer))
            {
                Vector3 dir = Quaternion.Inverse(transform.rotation) * ray.direction / 2;
                Vector3 point = Quaternion.Inverse(transform.rotation) * (hit.point - transform.position) * gen.size + Vector3.one * gen.size / 2.0f;

                while (true)
                {
                    Vector3Int rPos = gen.ClampToBounds(RoundVec(point));
                    Vector3 nextPos = point + dir;
                    Vector3Int nextRPos = RoundVec(nextPos);

                    if (gen.IsOutOfBounds(nextRPos.x, nextRPos.y, nextRPos.z) || gen.vox[nextRPos.x][nextRPos.y][nextRPos.z])
                    {
                        if (Input.GetButtonDown(place))
                        {
                            Place(rPos, hit);
                        }
                        else if (Input.GetButtonDown(remove))
                        {
                            Remove(nextRPos, hit);
                        }
                        break;
                    }
                    point += dir;
                }
                gen.Generate();
            }
        }
    }
    private void Place(Vector3Int rPos, RaycastHit hit)
    {
        if (GlobalStats.self.Tiles <= 0) return;

        if (gen.IsOutOfBounds(rPos.x, rPos.y, rPos.z)) return;

        ref bool tile = ref gen.vox[rPos.x][rPos.y][rPos.z];
        if (tile) return;
        tile = true;

        onPlace.Invoke(hit.point);
        GlobalStats.self.Tiles -= 1;
    }
    private void Remove(Vector3Int nextRPos, RaycastHit hit)
    {
        if (gen.IsOutOfBounds(nextRPos.x, nextRPos.y, nextRPos.z)) return;

        ref bool tile = ref gen.vox[nextRPos.x][nextRPos.y][nextRPos.z];
        tile = false;
        onBreak.Invoke(hit.point);

        GlobalStats.self.Tiles += 1;
    }
}
