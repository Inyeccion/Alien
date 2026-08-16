//Runtime Node
using System.Collections.Generic;
using UnityEngine;

public class BTNode
{
    public NodeType nodeType;

    public List<BTNode> children;

    public virtual NodeState Tick()
    {
        Debug.Log("BTNode: Blank Tick");
        return NodeState.succeed;
    }
}
