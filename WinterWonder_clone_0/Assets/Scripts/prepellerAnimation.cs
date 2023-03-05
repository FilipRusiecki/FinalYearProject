using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prepellerAnimation : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.transform.rotation = new Quaternion(0.0f,0.0f,1.0f,0);
    }
}
