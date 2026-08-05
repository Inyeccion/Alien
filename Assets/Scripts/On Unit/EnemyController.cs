using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MoveMent, IPossessable
{
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public void OnEnterHost()
    {
        hostManager.SetHost(this);
        //可以用条件判断来做：一个宿主进入另一个时的逻辑
        Debug.Log("EnemyController: Entering host.");
    }

    public void OnExitHost()
    {
        //退出寄生时的逻辑
        Debug.Log("EnemyController: Exiting host.");
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
