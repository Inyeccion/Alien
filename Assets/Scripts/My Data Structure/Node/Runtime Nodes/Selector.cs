using System.Collections.Generic;

//这里初步设计成
public class Selector : BTNode
{
    public int currentChildrenInd = 0;
    public override NodeState Tick()
    {
        while (currentChildrenInd < children.Count)
        {
            NodeState state = children[currentChildrenInd].Tick();

            if (state is NodeState.running)
            {
                currentChildrenInd = 0;
                return NodeState.running;
            }
            if (state is NodeState.succeed)
            {
                currentChildrenInd = 0;
                return NodeState.succeed;
            }
            currentChildrenInd++;
        }
        currentChildrenInd = 0;
        return NodeState.failed;
    }
    public Selector()
    {
        nodeType = NodeType.selector;

        children = new List<BTNode>();
    }
}
