//RootNode只能有一个孩子

public class RootNode : BTNode
{

    public override NodeState Tick()
    {
        return children[0].Tick();
    }

    public RootNode()
    {
        nodeType = NodeType.rootNode;
    }
}
