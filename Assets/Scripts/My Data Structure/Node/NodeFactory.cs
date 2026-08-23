using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public static class NodeFactory
{
    public static BTNode CreateNode(BTNodeDataSO currentNodeData, AIContext AIContext)
    {
        if (currentNodeData.nodeType == NodeType.chaseNode)
        {
            ChaseNode chaseNode = new ChaseNode();
            if (currentNodeData is ChaseNodeDataSO chaseNodeData)
                chaseNode.Initialize(chaseNodeData.distanceStopChase, AIContext);
            return chaseNode;
        }
        if (currentNodeData.nodeType == NodeType.attackNode)
        {
            AttackNode attackNode = new AttackNode();
            return attackNode;
        }
        if (currentNodeData.nodeType == NodeType.selector)
        {
            Selector selector = new Selector();
            return selector;
        }
        if (currentNodeData.nodeType == NodeType.rootNode)
        {
            RootNode rootNode = new RootNode();
            return rootNode;
        }
        Debug.LogWarning("NodeFactory: nodeType mismatch, creating a root BTNode");
        return new RootNode();
    }
}
