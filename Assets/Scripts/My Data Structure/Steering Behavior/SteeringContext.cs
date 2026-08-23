using UnityEngine;

public class SteeringContext
{
    //MovementType
    public MovementType movementType;
    //自身数据
    public Vector3 currentPos;
    public Vector3 currentVelocity;

    //目标数据
    public Vector3 targetPos;
    public Vector3 targetVelocity;

    //Steering Behavior
    public float maxSpeed;
    public float maxDeltaVelocity;

    public Transform targetTransform;

    public void ResetContext()
    {
        //注意movementType第一个必须设置成Idle状态
        movementType = 0;

        currentPos = Vector3.zero;
        currentVelocity = Vector3.zero;

        targetPos = Vector3.zero;
        targetVelocity = Vector3.zero;

        maxSpeed = 0;
        maxDeltaVelocity = 0;

        targetTransform = null;
    }

}
