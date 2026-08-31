//Runtime Node
using System.Collections.Generic;


public abstract class BTNode
{
    public NodeType nodeType;

    public List<BTNode> children;

    //由RuntimeTree的初始化负责传入引用
    public AIContext AIContext;
    public abstract NodeState Tick();

    public abstract void Reset();

}
