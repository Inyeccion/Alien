using UnityEngine;
[CreateAssetMenu(fileName = "BehaviorTree", menuName = "Scriptable Object/Behavior Tree/Behavior Tree Data")]
public class BehaviorTreeSO : ScriptableObject
{
    public BTNodeDataSO root;

    //遍历初始化树，返回根节点的引用
    public BTNode InitializeTree(BTNodeDataSO currentNodeData, AIContext AIContext)
    {
        //debug
        if (currentNodeData == null)
        {
            Debug.LogWarning("BehaviorTreeSO: currentNodeData is null");
            return null;
        }
        //先把根创建好
        BTNode currentNode = NodeFactory.CreateNode(currentNodeData, AIContext);

        //叶子结点直接返回
        if (currentNodeData.children == null) return currentNode;
        //左右  遍历当前结点的所有子结点，但是应该不会遍历到空结点
        for (int i = 0; i < currentNodeData.children.Count; i++)
        {
            currentNode.children.Add(InitializeTree(currentNodeData.children[i], AIContext));
        }

        //根
        return currentNode;
    }
}
