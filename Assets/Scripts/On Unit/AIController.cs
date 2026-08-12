using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private BehaviorTreeSO behaviorTree;

    public BlackBoard blackBoard;

    private void Start()
    {
        InitializeBlackBoard();
    }

    private void InitializeBlackBoard()
    {
        blackBoard = new BlackBoard
        {
            abilitySystem = GetComponent<AbilitySystem>(),
            characterMotor = GetComponent<CharacterMotor>(),
            target = HostManagerSO.currentHost.transform,
            distanceToTarget = (HostManagerSO.currentHost.transform.position - transform.position).magnitude,
            targetPos = HostManagerSO.currentHost.transform.position,
        };

    }
}
