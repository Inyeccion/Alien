using UnityEngine;

public class HostCore : MonoBehaviour, IPossessable
{
    private AIController AIController;
    private MovementController movementController;
    private SensorSystem sensorSystem;

    private void Start()
    {
        AIController = gameObject.GetComponent<AIController>();
        movementController = gameObject.GetComponent<MovementController>();
        sensorSystem = gameObject.GetComponent<SensorSystem>();
    }
    //设置currentHost，关闭AI系统
    public void OnEnterHost()
    {
        HostManagerSO.SetHost(this);

        if (AIController is not null)
        {
            sensorSystem.ShutDown();
            AIController.ShutDown();
            movementController.ShutDown();
            Debug.Log("CharacterMotor: SensorSystem, AIController and MovementController are Shutting Down.");
        }
    }

    //退出寄生
    public void OnExitHost()
    {
        sensorSystem.ReBoot();
        AIController.ReBoot();
        movementController.ReBoot();
    }

    public bool IsPlayer()
    {
        if (gameObject.name != "Player")
        {
            return false;
        }
        return true;
    }
}
