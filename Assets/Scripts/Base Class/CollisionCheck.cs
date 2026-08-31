using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CollisionCheck : MonoBehaviour
{
    private float skinWidth = 0.02f;
    private float playerWallAngle;
    private Vector3 wallNormal;
    protected CapsuleCollider capsuleCollider;
    [SerializeField]
    protected Vector3 center;

    [SerializeField]
    protected float height;

    [SerializeField]
    protected LayerMask collisionMask;

    [SerializeField]
    protected Vector3 top;

    [SerializeField]
    protected Vector3 bottom;

    [SerializeField]
    protected float radius;

    protected Rigidbody rb;

    [SerializeField] protected HostManagerSO hostManager;


    protected void CalculateForCast()
    {
        //为移动检测做计算
        center = transform.TransformPoint(capsuleCollider.center);

        height = capsuleCollider.height * transform.lossyScale.y;

        radius = capsuleCollider.radius * transform.lossyScale.x;

        top = center + Vector3.up * (height / 2 - radius);

        bottom = center - Vector3.up * (height / 2 - radius);
    }

    protected Vector3 CollisionSolver(Vector3 direction, Vector3 distance)
    {
        if (IsVelocityNegligible(distance))
        {
            if (this is CharacterMotor characterMotor)
                if (IsVelocityNegligible(characterMotor.externalVelocity))
                {
                    Debug.Log("CollisionCheck: externalVelocity IsVelocityNegligible, set it to zero");
                    characterMotor.SetExternalVelocity(Vector3.zero);
                }
            return Vector3.zero;
        }


        //没有检测到碰撞
        if (!Physics.CapsuleCast(bottom, top, radius, direction, out RaycastHit hitInfo, distance.magnitude, collisionMask))
        {
            if (this is CharacterMotor characterMotor) Debug.Log("CollisionCheck: 没有检测到碰撞");
            else Debug.LogWarning("CollisionCheck: this is not CharacterMotor");

            return distance;
        }
        else //检测到碰撞
        {
            //撞上
            Debug.Log("CollisionCheck: Wall has been detected. Collider Object name: " + hitInfo.collider.gameObject.name);

            float safeDistance = Mathf.Max(hitInfo.distance - skinWidth, 0);
            Vector3 awayFromCollision = direction * safeDistance;
            Vector3 restFromDistance = distance - awayFromCollision;
            //计算剩下的速度（距离）
            wallNormal = hitInfo.normal;
            Vector3 adjustedRestDistance = ClipVelocity(restFromDistance, wallNormal);
            //实时计算externalVelocity的变化
            if (this is CharacterMotor characterMotor)
            {
                Debug.Log("CollisionCheck: 检测到墙壁碰撞, 分解并调整externalVelocity");
                characterMotor.SetExternalVelocity(ClipVelocity(characterMotor.externalVelocity, wallNormal));
            }

            return awayFromCollision + CollisionSolver(CalculateDirection(adjustedRestDistance), adjustedRestDistance);
            
        }
    }

    private Vector3 ClipVelocity(Vector3 velocity,Vector3 normal)
    {
        float intoWall = Vector3.Dot(velocity, normal);

        if (intoWall < 0)
        {
            velocity -= normal * intoWall;
        }
        return velocity;
    }

    protected Vector3 CalculateDirection(Vector3 velocity)
    {
        return velocity.normalized;
    }

    protected bool IsVelocityNegligible(Vector3 velocity)
    {
        return velocity.magnitude < 0.01f;
    }
}