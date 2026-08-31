using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "HostManager", menuName = "Scriptable Object/Host Manager")]
public class HostManagerSO : ScriptableObject
{
    public static IPossessable currentHost { get; private set; }

    [HideInInspector]public static InputLayer inputLayer;

    public static bool isPossessing = false;

    [SerializeField] private static float timeScale = 0.5f;
    private static bool isTimeSlow = false;

    //引用InputLayer
    public static void SetInputLayer(InputLayer layer)
    {
        inputLayer = layer;
    }

    //移动
    public static void InformMotorToProcessEmptyMoveAction()
    {
        if (currentHost != null)
            currentHost.gameObject.GetComponent<CharacterMotor>().PlayerApplyDesiredVelocityToInternalVelocity(Vector2.zero);
        else Debug.Log("HostManagerSO: No current host to process move action.");
    }

    public static void InformMotorToProcessMoveAction(Vector2 action)
    {
        //暂时用条件判断来解决
        if (currentHost != null)
            currentHost.gameObject.GetComponent<CharacterMotor>().PlayerApplyDesiredVelocityToInternalVelocity(action);
        else Debug.LogWarning("HostManagerSO: No current host to process move action.");
    }

    //退出寄生
    public static void ExitHost()
    {
        if (isPossessing)
        {
            currentHost.OnExitHost();
            currentHost = null;
            isPossessing = false;
            Debug.Log("HostManagerSO: currentHost is null");
        }
    }

    //设置寄生控制权
    public static void SetHost(IPossessable nextHost)
    {
        currentHost = nextHost;
        Debug.Log("HostManagerSO: currentHost: " + currentHost.ToString());
        //条件判断根据后续开发需求来动态更改  高概率导致bug
        if (currentHost.IsPlayer())
        {
            isPossessing = false;
        }
        else
            isPossessing = true;
        Debug.Log("HostManagerSO: isPossessing = " + isPossessing);

    }
    //核心的设置寄生体方法
    public static void RayCastToSetHost()
    {
        if (!isPossessing)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                HostCore hostCore = hitInfo.collider.GetComponent<HostCore>();
                if (hostCore != null)
                {
                    Debug.Log("HostManagerSO: hitInfo Name: " + hitInfo.collider.name);

                    hostCore.OnEnterHost();
                }
            }
        }
    }
    //时间控制
    public static void SlowTimeScale()
    {
        if (!isTimeSlow)
        {
            Time.timeScale = timeScale;
            Time.fixedDeltaTime *= timeScale;
            isTimeSlow = true;

            inputLayer.hostAction.Enable();  //在时间放慢的时候才启用寄生功能
            Debug.Log("HostManagerSO: Time Slowed!");
        }
    }
    public static void ResetTimeScale()
    {
        if (isTimeSlow)
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime /= timeScale;
            isTimeSlow = false;

            inputLayer.hostAction.Disable(); //在时间恢复的时候禁用寄生功能
            Debug.Log("HostManagerSO: Time Reset!");
        }
    }


}
