using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard
    {
        [HideInInspector]
        public AI ai;
        public List<Transform> targetList;

        [Header("Movement data--------")]
        public Vector2 movementTarget;
        //[HideInInspector]
        public Vector2 lastSeenPosition;
        public bool haveLastSeenPos;
        //public Transform playerPos;
        public Collider2D randomArea;
        //[HideInInspector]
        public Vector2 randomPosition;

        [Header("Combat data--------")]
        public bool beingHit;
        public bool enlessChase;
        [HideInInspector]
        public Vector2 bulletPos;
    }
}