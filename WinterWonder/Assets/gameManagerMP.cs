using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Mirror.Examples.Basic
{
    public class gameManagerMP : NetworkBehaviour
    {
        [Header("Multiplayer Vars")]
        [SyncVar]
        public Vector3 syncPosition;

        public PlayerController multiPlayer;
        public List<GameObject> multiplayerS;

        // Start is called before the first frame update
        void Start()
        {
            if (!isServer)
            {
                return;
            }
            multiPlayer = FindObjectOfType<PlayerController>();
            multiplayerS.Add(GameObject.FindGameObjectWithTag("Player"));
        }

        // Update is called once per frame
        void Update()
        {
            if (!isServer)
            {
                return;
            }
        }

    }
}