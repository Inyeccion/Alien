using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MoveMent,IPossessable
{
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }
    //初始化逻辑外层可以放在这里？
    public void OnEnterHost()
    {
        hostManager.SetHost(this);
    }


    //由于isPossessing不会触发
    public void OnExitHost() { }  
    

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
