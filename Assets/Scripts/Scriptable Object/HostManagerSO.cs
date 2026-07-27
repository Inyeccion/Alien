using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "HostManager", menuName = "HostManager")]
public class HostManagerSO : ScriptableObject
{
    private IPossessable currentHost;

    public void ProcessMoveAction(Vector2 action)
    {
        currentHost.OnMoveInput(action);
    }

    public void SetHost(IPossessable nextHost)
    {
        currentHost = nextHost;
    }

    
}
