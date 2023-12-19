using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Generation gen;
    [SerializeField] private LayerMask buildingLayer;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    private Vector3Int RoundVec(Vector3 target)
    {
        return new Vector3Int(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y), Mathf.FloorToInt(target.z));
    }
    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50.0f, buildingLayer))
        {
            Vector3 dir = ray.direction / 2.0f;
            Vector3 point = hit.point * gen.size + Vector3.one * gen.size / 2.0f;

            while (true)
            {
                Vector3Int rPos = gen.ClampToBounds(RoundVec(point));
                Vector3 nextPos = point + dir;
                Vector3Int nextRPos = RoundVec(nextPos);

                if (gen.IsOutOfBounds(nextRPos.x, nextRPos.y, nextRPos.z) || gen.vox[nextRPos.x][nextRPos.y][nextRPos.z])
                {
                    if (gen.IsOutOfBounds(rPos.x, rPos.y, rPos.z)) break;
                    gen.vox[rPos.x][rPos.y][rPos.z] = true;
                    break;
                }
                point += dir;
            }
            gen.Generate();
        } 
    }
}
