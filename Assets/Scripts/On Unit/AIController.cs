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
    //初始化运行时决策树
    private void InitializeRuntimeTree()
    {
        BTNode root = behaviorTree.InitializeTree(behaviorTree.root, AIContext);
        runtimeBehaviorTree = new RuntimeBehaviorTree(root);
    }
    //初始化AIContext
    private void InitializeContext()
    {
        AIContext.abilitySystem = GetComponent<AbilitySystem>();
        AIContext.characterMotor = GetComponent<CharacterMotor>();
        AIContext.movementController = GetComponent<MovementController>();
        InitializeBlackBoard(AIContext.blackBoard);
    }
    //初始化决策所需的数据
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
