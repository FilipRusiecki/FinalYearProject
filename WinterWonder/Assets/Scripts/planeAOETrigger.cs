using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class planeAOETrigger : MonoBehaviour
{

        public PlayerController player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
         player = PlayerController.FindObjectOfType<PlayerController>();
       }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player.inPlane = true;
            }
    }

}
