using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // bullet.AddForce(Vector3.forward * 500);
        transform.Translate(Vector3.forward * 80 * Time.deltaTime);

    }
}
