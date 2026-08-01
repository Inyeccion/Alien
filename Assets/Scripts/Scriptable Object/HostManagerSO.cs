using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "HostManager", menuName = "HostManager")]
public class HostManagerSO : ScriptableObject
{
    private IPossessable currentHost;

    public InputLayer inputLayer;

    public bool isPossessing = false;

    [SerializeField] private float timeScale = 0.5f;
    private bool isTimeSlow = false;

    //引用InputLayer
    public void SetInputLayer(InputLayer inputLayer)
    {
        this.inputLayer = inputLayer;
    }

    //移动
    public void ProcessMoveAction(Vector2 action)
    {
        //暂时用条件判断来解决
        if (currentHost != null)
            currentHost.OnMoveInput(action);
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
        Debug.Log(currentHost.ToString());
        //条件判断根据后续开发需求来动态更改  高概率导致bug
        if (currentHost.gameObject.name != "Player")
            isPossessing = true;
        else
            isPossessing = false;
        Debug.Log("isPossessing = " + isPossessing);

    }
    //核心的设置寄生体方法
    public void RayCastToSetHost()
    {
        Debug.Log("hostAction Pressed!");
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.GetComponent<EnemyController>() != null)
            {
                Debug.Log("hitInfo Name: " + hitInfo.collider.name);
                EnemyController enemyController = hitInfo.collider.GetComponent<EnemyController>();
                enemyController.OnEnterHost();
            }
            else if (hitInfo.collider.GetComponent<PlayerController>() != null)
            {
                Debug.Log("hitInfo Name: " + hitInfo.collider.name);
                PlayerController playerController = hitInfo.collider.GetComponent<PlayerController>();
                playerController.OnEnterHost();
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
            Debug.Log("Time Slowed!");
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
            Debug.Log("Time Reset!");
        }
    }

#if UNITY_EDITOR
    //Debug
    public string returnCurrentHost()
    {
        string ret = currentHost.ToString();
        return ret;
    }
#endif
}
