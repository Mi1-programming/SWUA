using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("elf has begun");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        print("colliding");
    }
}
