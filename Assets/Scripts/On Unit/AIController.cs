using System.Threading;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private BehaviorTreeSO behaviorTree;

    private RuntimeBehaviorTree runtimeBehaviorTree;
    [SerializeField] private float behaviorTreeInterval = 0.1f;
    [SerializeField] private float behaviorTreeTimer = 0;

    private SensorSystem sensorSystem;

    public AIContext AIContext;

    private void Start()
    {
        sensorSystem = GetComponent<SensorSystem>();
        InitializeContext();
        sensorSystem.Initialize(AIContext.blackBoard);
        InitializeRuntimeTree();
    }

    private void Update()
    {
        behaviorTreeTimer += Time.deltaTime;
        if (behaviorTreeTimer > behaviorTreeInterval)
        {
            runtimeBehaviorTree.Tick();
            behaviorTreeTimer = 0;
        }

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
