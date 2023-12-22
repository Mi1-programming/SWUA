using UnityEngine;
using System.Collections.Generic;

public class DraggableBody : MonoBehaviour
{
    public static List<DraggableBody> bodies = new List<DraggableBody>();
    [HideInInspector] public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        bodies.Add(this);
    }
    private void OnDisable()
    {
        bodies.Remove(this);
    }
}
