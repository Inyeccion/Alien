//½áµãData assetÄ£°å

using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BTNodeData", menuName = "Scriptable Object/Behavior Tree/BTNode Data")]
public class BTNodeDataSO : ScriptableObject
{
    public NodeType nodeType;

    public List<BTNodeDataSO> children;
}
