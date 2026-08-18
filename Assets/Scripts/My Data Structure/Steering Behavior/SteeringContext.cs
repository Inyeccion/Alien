using UnityEngine;

public class SteeringContext
{
    //自身数据
    public Vector3 currentPos;
    public Vector3 currentVelocity;

    //目标数据
    public Vector3 targetPos;
    public Vector3 targetVelocity;

    //Steering Behavior
    public float maxSpeed;
    public float maxDeltaVelocity;

    public Transform transform;
}
