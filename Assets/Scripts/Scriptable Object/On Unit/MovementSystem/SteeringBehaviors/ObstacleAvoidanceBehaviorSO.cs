using UnityEngine;

[CreateAssetMenu(fileName = "SpecificObstacleAvoidanceBehavior", menuName = "Scriptable Object/Steering Behaviors/Specific Obstacle Avoidance Behavior")]
public class ObstacleAvoidanceBehaviorSO : SteeringBehaviorSO
{
    [SerializeField] private float baseLookAheadDistance;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float avoidanceStrength;
    [SerializeField] private AvoidanceSide side;

    public override SteeringOutput Calculate(SteeringContext context)
    {
        //碰撞查询
        Vector3 direction = context.currentPrimaryDesiredVelocity.normalized;

        CapsuleHit capsuleHit = context.currentCharacterMotor.PerformObstacleAvoidanceCast(direction, baseLookAheadDistance, obstacleLayer);

        if (!capsuleHit.hit)
        {
            return new SteeringOutput(Vector3.zero, Vector3.zero);
        }

        //计算避让方向
        Vector3 rightTangent = Vector3.Cross(capsuleHit.hitInfo.normal, Vector3.up).normalized;
        Vector3 leftTangent = -rightTangent;

        //选择偏好避让方向
        //初次选择
        if (side == AvoidanceSide.None)
        {
            side = ChooseSide(direction, leftTangent, rightTangent);
        }
        //上一次避让方向side影响下次避让
        
        Vector3 avoidanceDirection = (side == AvoidanceSide.Left) ? leftTangent : rightTangent;


        //计算避让强度
        float urgency = 1f - Mathf.Clamp01(capsuleHit.hitInfo.distance / baseLookAheadDistance);

        //计算避让速度
        Vector3 avoidanceVelocity = avoidanceDirection * urgency * avoidanceStrength;

        return new SteeringOutput(avoidanceVelocity, Vector3.zero);

    }

    private AvoidanceSide ChooseSide(Vector3 direction, Vector3 leftTangent, Vector3 rightTangent)
    {
        float leftDot = Vector3.Dot(direction, leftTangent);
        float rightDot = Vector3.Dot(direction, rightTangent);
        if (leftDot > rightDot)
        {
            return AvoidanceSide.Left;
        }
        else
        {
            return AvoidanceSide.Right;
        }
    }

    private enum AvoidanceSide
    {
        None,
        Left,
        Right
    }
}