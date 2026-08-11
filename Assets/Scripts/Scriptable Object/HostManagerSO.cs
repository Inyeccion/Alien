using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "HostManager", menuName = "Scriptable Object/Host Manager")]
public class HostManagerSO : ScriptableObject
{
    public static IPossessable currentHost { get; private set; }

    [HideInInspector]public InputLayer inputLayer;

    public bool isPossessing = false;

    [SerializeField] private float timeScale = 0.5f;
    private bool isTimeSlow = false;

    //引用InputLayer
    public void SetInputLayer(InputLayer inputLayer)
    {
        this.inputLayer = inputLayer;
    }

    //移动
    public static void InformMotorToProcessMoveAction(Vector2 action)
    {
        //暂时用条件判断来解决
        if (currentHost != null)
            currentHost.gameObject.GetComponent<CharacterMotor>().AddInternalVelocity(action);
        else Debug.LogWarning("HostManagerSO: No current host to process move action.");
    }

    //退出寄生
    public void ExitHost()
    {
        if (isPossessing)
        {
            currentHost.OnExitHost();
            isPossessing = false;
        }
    }

    //设置寄生控制权
    public void SetHost(IPossessable nextHost)
    {
        currentHost = nextHost;
        Debug.Log("HostManagerSO: currentHost: " + currentHost.ToString());
        //条件判断根据后续开发需求来动态更改  高概率导致bug
        if (currentHost.gameObject.name != "Player")
            isPossessing = true;
        else
            isPossessing = false;
        Debug.Log("HostManagerSO: isPossessing = " + isPossessing);

    }
    //核心的设置寄生体方法
    public void RayCastToSetHost()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            CharacterMotor characterMotor = hitInfo.collider.GetComponent<CharacterMotor>();
            if (characterMotor != null)
            {
                Debug.Log("HostManagerSO: hitInfo Name: " + hitInfo.collider.name);
                
                characterMotor.OnEnterHost();
            }
        }
    }
    //时间控制
    public void SlowTimeScale()
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
    public void ResetTimeScale()
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
