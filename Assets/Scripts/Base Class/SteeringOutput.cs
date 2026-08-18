using UnityEngine;


public class SteeringOutput 
{
    public Vector3 desiredVelocity = Vector3.zero;
    public Vector3 angular = Vector3.zero;
    public SteeringOutput(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        desiredVelocity = linearVelocity;
        angular = angularVelocity;
    }
}
