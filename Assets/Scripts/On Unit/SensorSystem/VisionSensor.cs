using UnityEngine;

public class VisionSensor : ISensor
{
    private BlackBoard blackBoard;

    private float interval = 0.1f;
    private float timer = 0;

    //传入AIController层的BB引用
    public void Initialize(BlackBoard board)
    {
        blackBoard = board;
    }

    public void UpdateSensor(float deltaTime)
    {
        timer += deltaTime;

        if (timer > interval)
        {
            DetectTargetGlobal();
            DetectThreatGlobal();
            timer = 0;
        }
    }

    public void Clear()
    {
        blackBoard.target = null;
    }

    //视觉语义上检测整个房间的目标
    private void DetectTargetGlobal()
    {
        if (HostManagerSO.currentHost != null)
        {
            blackBoard.target = HostManagerSO.currentHost.transform;
        }
    }

    //视觉语义上检测整个房间的威胁
    private void DetectThreatGlobal()
    {
        if (HostManagerSO.currentHost != null)
        {
            blackBoard.threat = HostManagerSO.currentHost.transform;
        }
    }
}
