using UnityEngine;

public class HostCore : MonoBehaviour, IPossessable
{
    private AIController AIController;
    private MovementController movementController;

    private void Start()
    {
        AIController = gameObject.GetComponent<AIController>();
        movementController = gameObject.GetComponent<MovementController>();
    }
    //设置currentHost，关闭AI系统
    public void OnEnterHost()
    {
        HostManagerSO.SetHost(this);

        if (AIController is not null)
        {
            AIController.ShutDown();
            movementController.ShutDown();
            Debug.Log("CharacterMotor: AIController and MovementController Shutting Down.");
        }
    }

    //退出寄生
    public void OnExitHost()
    {
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
