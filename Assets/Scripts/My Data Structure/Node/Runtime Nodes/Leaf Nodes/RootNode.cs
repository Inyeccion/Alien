//RootNode只能有一个孩子
using System.Collections.Generic;
public class RootNode : BTNode
{

    public override NodeState Tick()
    {
        return children[0].Tick();
    }

    public override void Reset()
    {
        foreach (BTNode child in children)
        {
            child.Reset();
        }
    }

    public RootNode()
    {
        nodeType = NodeType.rootNode;

        children = new List<BTNode>();
    }
}
