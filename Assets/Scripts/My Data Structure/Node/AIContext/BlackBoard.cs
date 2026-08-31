// AI认知世界的信息  数据黑板
using UnityEngine;

[System.Serializable]
public class BlackBoard
{
    public Transform target;                    //当前的目标对象
    public float distanceToTarget;              //目标对象的距离
    public Vector3 targetPos;                   //当前的目标位置（要去哪里）

    public Transform threat;
    public float distanceToThreat;
    public Vector3 threatPos;

    public void Reset()
    {
        target = null;
        distanceToTarget = 0;
        targetPos = Vector3.zero;

        threat = null;
        distanceToThreat = 0;
        threatPos = Vector3.zero;
    }
}
