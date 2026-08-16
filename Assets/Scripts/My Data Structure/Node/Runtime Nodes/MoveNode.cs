using UnityEngine;

public class MoveNode : BTNode
{
    public override NodeState Tick()
    {


        return NodeState.failed;
    }

    public MoveNode()
    {
        nodeType = NodeType.moveNode;
    }
}
