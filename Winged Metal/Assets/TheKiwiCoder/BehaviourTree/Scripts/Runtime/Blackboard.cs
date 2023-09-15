using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {
        public Collider2D randomArea;
        public Transform randomPosition;
        public Transform playerPos;
        public Vector2 target;
        public bool openFire;
        public LayerMask bulletBlock;
        public LayerMask viewBlock;
    }
}