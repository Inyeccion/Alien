using UnityEngine;

[CreateAssetMenu(fileName = "ChaseNodeData", menuName = "Scriptable Object/Behavior Tree/Chase Node Data")]
public class ChaseNodeDataSO : BTNodeDataSO
{
    //物体与目标的距离大于这个值就停止chase，返回failed
    public float distanceStopChase;
}

[CreateAssetMenu(fileName = "FleeNodeData", menuName = "Scriptable Object/Behavior Tree/Flee Node Data")]
public class FleeNodeDataSO : BTNodeDataSO
{
    public float distanceStopFlee;
}