using UnityEngine;

public class RuntimeBehaviorTree
{
    public BTNode root;

    public RuntimeBehaviorTree(BTNode node)
    {
        root = node;
    }

    public NodeState Tick()
    {
        return root.Tick();
    }

    public void Reset()
    {
        root.Reset();
    }
}
