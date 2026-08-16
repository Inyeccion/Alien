using UnityEngine;
public static class NodeFactory
{
    public static BTNode CreateNode(NodeType nodeType)
    {
        if (nodeType == NodeType.attackNode)
        {
            AttackNode attackNode = new AttackNode();
            return attackNode;
        }
        if (nodeType == NodeType.selector)
        {
            Selector selector = new Selector();
            return selector;
        }
        Debug.LogWarning("NodeFactory: nodeType mismatch, creating a root BTNode");
        return new RootNode();
    }
}
