using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {
        [Header("Target data--------")]
        public Vector2 target;
        public float detectingDistance;   
        public float guardingViewDistance;
        public float attackingDistance;
        [HideInInspector]
        public Vector2 lastSeenPosition;
        public bool haveLastSeenPos;
        public Transform playerPos;
        public Collider2D randomArea;
        [HideInInspector]
        public Vector2 randomPosition;

        [Header("Combat data--------")]
        public bool openFire;
        public LayerMask bulletBlock;
        public LayerMask viewBlock;
        public float currentEn;
        public bool beingHit;
        [HideInInspector]
        public Vector2 bulletPos;
    }
}