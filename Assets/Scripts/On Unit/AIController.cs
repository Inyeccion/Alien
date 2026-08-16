using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private BehaviorTreeSO behaviorTree;

    private RuntimeBehaviorTree runtimeBehaviorTree;

    public AIContext AIContext;

    private void Start()
    {
        InitializeContext();
        InitializeRuntimeTree();
    }

    private void InitializeRuntimeTree()
    {
        BTNode root = behaviorTree.InitializeTree(behaviorTree.root);
        runtimeBehaviorTree = new RuntimeBehaviorTree(root);
    }

    private void InitializeContext()
    {
        AIContext.abilitySystem = gameObject.GetComponent<AbilitySystem>();
        AIContext.characterMotor = gameObject.GetComponent<CharacterMotor>();
        InitializeBlackBoard(AIContext.blackBoard);
    }

    private void InitializeBlackBoard(BlackBoard blackBoard)
    {
        blackBoard = new BlackBoard
        {
            target = HostManagerSO.currentHost.transform,
            distanceToTarget = (HostManagerSO.currentHost.transform.position - transform.position).magnitude,
            targetPos = HostManagerSO.currentHost.transform.position,
        };

    }

    public void ShutDown()
    {
        this.enabled = false;
    }
}
