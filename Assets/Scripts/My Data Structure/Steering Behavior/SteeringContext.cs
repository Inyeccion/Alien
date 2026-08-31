using UnityEngine;

[System.Serializable]
public class SteeringContext
{
    //自身数据
    public Vector3 currentPos;
    public Vector3 currentVelocity;

    //目标数据
    public Transform targetTransform;
    public Vector3 targetPos;
    public Vector3 targetVelocity;

    //Steering Behavior
    public float maxSpeed;
    public float acceleration;


    public SteeringContext(Transform currentTransform)
    {
        currentPos = currentTransform.position;
        targetPos = currentPos;
    }

    public void ResetContext()
    {
        currentPos = Vector3.zero;
        currentVelocity = Vector3.zero;

        targetTransform = null;
        targetPos = Vector3.zero;
        targetVelocity = Vector3.zero;

        maxSpeed = 0;
        acceleration = 0;

    }

}
