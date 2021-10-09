using UnityEngine;

namespace DefaultNamespace
{
    public class State
    {
        public State(Vector3 posVector, Vector3 velocityVector)
        {
            this.posVector = posVector;
            this.velocityVector = velocityVector;
        }

        public Vector3 posVector { get; set; }
        public Vector3 velocityVector { get; set; }
    }
}