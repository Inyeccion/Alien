//Runtime Node
using System.Collections.Generic;


public abstract class BTNode
{
    public NodeType nodeType;

    public List<BTNode> children;

    public AIContext AIContext;
    public abstract NodeState Tick();

}
