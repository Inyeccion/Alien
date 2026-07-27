using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMent : MonoBehaviour, IPossessable
{
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

    [SerializeField]
    protected float speed = 10.0f;

    [SerializeField]
    protected InputAction moveAction;

    protected Rigidbody rb;

    [SerializeField] protected HostManagerSO HostManager;


    protected void CalculateForCast()
    {
        //为移动检测做计算
        center = transform.TransformPoint(capsuleCollider.center);

        height = capsuleCollider.height * transform.lossyScale.y;

        radius = capsuleCollider.radius * transform.lossyScale.x;

        top = center + Vector3.up * (height / 2 - radius);

        bottom = center - Vector3.up * (height / 2 - radius);
    }

    protected void EnvironmentCollisionCheck(Vector3 direction, Vector3 distance)
    {
        //没有检测到墙壁碰撞
        if (!Physics.CapsuleCast(bottom, top, radius, direction, out RaycastHit hitInfo, distance.magnitude, collisionMask))
        {

            rb.MovePosition(rb.position + distance);
        }
        else //检测到墙壁碰撞
        {
            //正着撞上
            Debug.Log(hitInfo.collider.gameObject.name);

            //斜着撞上
            if (Vector3.Angle(direction, hitInfo.normal) < 136)
            {
                //左负右正    可能是左手系导致
                Vector3 cross = Vector3.Cross(direction, hitInfo.normal);
                if (cross.y > 0)  //右
                {
                    //矫正方向
                    direction = Quaternion.AngleAxis(45, Vector3.up) * direction;
                    distance = direction * speed * Time.deltaTime;
                    if (!Physics.CapsuleCast(bottom, top, radius, direction, distance.magnitude, collisionMask))
                        rb.MovePosition(rb.position + distance);
                }
                else              //左
                {
                    //矫正方向
                    direction = Quaternion.AngleAxis(-45, Vector3.up) * direction;
                    distance = direction * speed * Time.deltaTime;
                    if (!Physics.CapsuleCast(bottom, top, radius, direction, distance.magnitude, collisionMask))
                        rb.MovePosition(rb.position + distance);
                }
            }
        }
    }

    public void OnMoveInput(Vector2 action)
    {
        //有移动
        if (action != Vector2.zero)
        {
            CalculateForCast();

            Vector3 direction = new Vector3(action.x, 0, action.y).normalized;
            Vector3 distance = direction * speed * Time.deltaTime;
            EnvironmentCollisionCheck(direction, distance);

        }
    }
}