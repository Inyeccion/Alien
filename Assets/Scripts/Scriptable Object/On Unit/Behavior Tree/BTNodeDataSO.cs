//½áµãData assetÄ£°å

using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NodeData", menuName = "Scriptable Object/Behavior Tree/Node Data")]
public class BTNodeDataSO : ScriptableObject
{
    public NodeType nodeType;

    public List<BTNodeDataSO> children;
}
