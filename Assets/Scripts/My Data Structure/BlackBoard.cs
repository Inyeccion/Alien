using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public AbilitySystem abilitySystem;         //技能系统
    public CharacterMotor characterMotor;       //移动组件

    public Transform target;                    //当前的目标对象
    public float distanceToTarget;              //目标对象的距离

    public Vector3 targetPos;                   //当前的目标位置（要去哪里）

}
