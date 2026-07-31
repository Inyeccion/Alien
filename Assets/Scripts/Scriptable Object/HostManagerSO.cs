using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "HostManager", menuName = "HostManager")]
public class HostManagerSO : ScriptableObject
{
    private IPossessable currentHost;

    [SerializeField] private float timeScale = 0.5f;
    private bool isTimeSlow = false;
    //移动
    public void ProcessMoveAction(Vector2 action)
    {
        currentHost.OnMoveInput(action);
    }

    //设置寄生控制权
    public void SetHost(IPossessable nextHost)
    {
        currentHost = nextHost;
    }

    public void RayCastToSetHost()
    {
        Debug.Log("hostAction Pressed!");
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider.GetComponent<EnemyController>() != null)
        {
            Debug.Log(hitInfo.collider.name);
            EnemyController enemyController = hitInfo.collider.GetComponent<EnemyController>();
            this.SetHost(enemyController);

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
