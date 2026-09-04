using UnityEngine;

[CreateAssetMenu(fileName = "SpecificWanderBehavior", menuName = "Scriptable Object/Steering Behaviors/Specific Wander Behavior")]
public class WanderBehaviorSO : SteeringBehaviorSO
{
    [SerializeField] private float wanderRadius;        //圆的周长
    [SerializeField] private float wanderDistance;      //圆心到单位的距离
    [SerializeField] private float wanderJitter;        //每次更新时，随机偏移的抖动系数

    [SerializeField] private Vector3 wanderDirection;   //以圆心为原点的单位方向向量

    public override SteeringOutput Calculate(SteeringContext context)
    {
        Vector2 random = Random.insideUnitCircle * wanderJitter;
        wanderDirection += new Vector3(random.x, 0f, random.y);

        wanderDirection.Normalize();

        Vector3 forward = context.currentMoveVec.normalized;
        if (forward == Vector3.zero)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            forward = new Vector3(randomDirection.x, 0f, randomDirection.y);
        }
        
        Vector3 circleCenter = context.currentPos + forward * wanderDistance;


        Vector3 wanderTarget = circleCenter + wanderRadius * wanderDirection;      //随机点在圆上的位置

        Vector3 desiredVelocity = (wanderTarget - context.currentPos).normalized * context.maxSpeed;

        return new SteeringOutput(desiredVelocity, Vector3.zero);
    }

}
