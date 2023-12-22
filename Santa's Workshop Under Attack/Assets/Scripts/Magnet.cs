using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private string activate;
    [SerializeField] private float force = 8.0f;
    [SerializeField] private float range = 8.0f;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float damping = 0.05f;
    [SerializeField] private string launch;
    [SerializeField] private float launchForce = 16.0f;
    [SerializeField] private AnimationCurve pushCurve;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (!Input.GetButtonDown(launch)) return;

        foreach (var db in DraggableBody.bodies)
        {
            float dist = pushCurve.Evaluate(1.0f - Mathf.Clamp01(Vector3.Distance(transform.position, db.rb.position) / range));
            Vector3 dir = cam.transform.forward;

            db.rb.AddForce(launchForce * dir * dist);
        }
    }
    private void FixedUpdate()
    {
        if (!Input.GetButton(activate) || Input.GetButton(launch)) return;

        foreach (var db in DraggableBody.bodies)
        {
            float dist = curve.Evaluate(1.0f - Mathf.Clamp01(Vector3.Distance(transform.position, db.rb.position) / range));
            Vector3 dir = Vector3.Normalize(transform.position - db.rb.position);

            db.rb.velocity /= 1.0f + damping;
            db.rb.AddForce(force * dir * dist);
        }
    }
}
