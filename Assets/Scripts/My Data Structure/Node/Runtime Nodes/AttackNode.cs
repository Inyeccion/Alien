

public class AttackNode : BTNode
{

    public override NodeState Tick()
    {
        return NodeState.succeed;
    }

    public AttackNode()
    {
        nodeType = NodeType.attackNode;
    }
}
