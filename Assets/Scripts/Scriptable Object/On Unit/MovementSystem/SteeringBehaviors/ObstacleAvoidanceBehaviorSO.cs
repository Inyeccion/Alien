using UnityEngine;

[CreateAssetMenu(fileName = "SpecificObstacleAvoidanceBehavior", menuName = "Scriptable Object/Steering Behaviors/Specific Obstacle Avoidance Behavior")]
public class ObstacleAvoidanceBehaviorSO : SteeringBehaviorSO
{
    [SerializeField] private float baseLookAheadDistance = 1.5f;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float avoidanceStrength;
    [SerializeField] private AvoidanceSide side;

    [SerializeField] private bool centerBlocked = false;
    [SerializeField] private bool leftBlocked = false;
    [SerializeField] private bool rightBlocked = false;

    [SerializeField] private int leftProbAngle = -35;
    [SerializeField] private int rightProbAngle = 35;

    [SerializeField] private float leftProbDistance = 1.5f;
    [SerializeField] private float rightProbDistance = 1.5f;

    public override SteeringOutput Calculate(SteeringContext context)
    {
        //碰撞查询
        Vector3 centerDirection = context.currentPrimaryDesiredVelocity.normalized;

        Vector3 leftDirection = Quaternion.Euler(0, leftProbAngle, 0) * centerDirection;
        Vector3 rightDirection = Quaternion.Euler(0, rightProbAngle, 0) * centerDirection;

        CapsuleHit centerCapsuleHit = context.currentCharacterMotor.PerformObstacleAvoidanceCast(centerDirection, baseLookAheadDistance, obstacleLayer);

        CapsuleHit leftCapsuleHit = context.currentCharacterMotor.PerformObstacleAvoidanceCast(leftDirection, leftProbDistance, obstacleLayer);

        CapsuleHit rightCapsuleHit = context.currentCharacterMotor.PerformObstacleAvoidanceCast(rightDirection, rightProbDistance, obstacleLayer);

        centerBlocked = centerCapsuleHit.hit;
        rightBlocked = rightCapsuleHit.hit;
        leftBlocked = leftCapsuleHit.hit;


        //如果正前方完全没有检测到障碍物，则不需要避让，直接返回零向量
        if (!centerBlocked)
        {
            return new SteeringOutput(Vector3.zero, Vector3.zero);
        }

        //下面进入避让逻辑
        //计算避让方向
        Vector3 rightTangent = Vector3.Cross(centerCapsuleHit.hitInfo.normal, Vector3.up).normalized;
        Vector3 leftTangent = -rightTangent;

        //选择偏好避让方向
        //初次选择
        if (side == AvoidanceSide.None)
        {
            side = ChooseSide(centerDirection, leftTangent, rightTangent);
        }

        if (side == AvoidanceSide.Left && leftBlocked && !rightBlocked)
        {
            side = AvoidanceSide.Right;
        }
        if (side == AvoidanceSide.Right && rightBlocked && !leftBlocked)
        {
            side = AvoidanceSide.Left;
        }


        //上一次避让方向side影响下次避让
        Vector3 avoidanceDirection = (side == AvoidanceSide.Left) ? leftTangent : rightTangent;


        //计算避让强度
        float urgency = 1f - Mathf.Clamp01(centerCapsuleHit.hitInfo.distance / baseLookAheadDistance);

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