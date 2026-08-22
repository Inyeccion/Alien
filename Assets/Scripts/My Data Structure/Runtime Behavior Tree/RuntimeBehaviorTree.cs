using UnityEngine;

public class RuntimeBehaviorTree : MonoBehaviour
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
}
