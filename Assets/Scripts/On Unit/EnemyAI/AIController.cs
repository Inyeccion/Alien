using System.Threading;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private BehaviorTreeSO behaviorTree;

    private RuntimeBehaviorTree runtimeBehaviorTree;
    [SerializeField] private float behaviorTreeInterval = 0.1f;
    [SerializeField] private float behaviorTreeTimer = 0;

    public AIContext AIContext;

    private void Awake()
    {
        InitializeContext();
        InitializeRuntimeTree();        
    }

    private void Update()
    {
        UpdateBlackBoardContext();
        RuntimeBehaviorTreeTick();
    }

    private bool isTargetExist()
    {
        if (AIContext.blackBoard.target == null) return false;
        return true;
    }

    private void UpdateBlackBoardContext()
    {
        if (isTargetExist())
        {
            AIContext.blackBoard.targetPos = AIContext.blackBoard.target.position;
            AIContext.blackBoard.distanceToTarget = (transform.position - AIContext.blackBoard.targetPos).magnitude;
        }
    }

    private void RuntimeBehaviorTreeTick()
    {
        behaviorTreeTimer += Time.deltaTime;
        if (behaviorTreeTimer > behaviorTreeInterval)
        {
            runtimeBehaviorTree.Tick();
            behaviorTreeTimer = 0;
        }
    }

    public BlackBoard GetBlackBoardRef()
    {
        return AIContext.blackBoard;
    }

    //初始化运行时决策树
    private void InitializeRuntimeTree()
    {
        BTNode root = behaviorTree.InitializeTree(behaviorTree.root, AIContext);
        runtimeBehaviorTree = new RuntimeBehaviorTree(root);
        Debug.Log("AIController: RuntimeBehaviorTree Initialized.");
    }
    //初始化AIContext
    private void InitializeContext()
    {
        AIContext = new AIContext();
        //这边的几个引用是否应该在这里维护需要考虑，关系到系统运行顺序的问题？
        AIContext.abilitySystem = GetComponent<AbilitySystem>();
        //AIContext.characterMotor = GetComponent<CharacterMotor>();
        AIContext.movementController = GetComponent<MovementController>();
        InitializeBlackBoard();
    }
    //初始化BB，现在暂时规定为全空
    private void InitializeBlackBoard()
    {
        AIContext.blackBoard = new BlackBoard();

    }

    public void ShutDown()
    {
        this.enabled = false;
    }
}
