

public class Selector : BTNode
{
    public int currentChildrenInd;
    public override NodeState Tick()
    {
        while (currentChildrenInd < children.Count)
        {
            NodeState state = children[currentChildrenInd].Tick();

            if (state is NodeState.running)
            {
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
    }
}
