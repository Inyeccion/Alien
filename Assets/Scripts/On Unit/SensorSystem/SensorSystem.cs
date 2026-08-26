using UnityEngine;
using System.Collections.Generic;
public class SensorSystem : MonoBehaviour
{
    public BlackBoard blackBoard;

    private List<ISensor> sensors;

    private void Awake()
    {
        InitializeSensorList();
    }

    private void Start()
    {
        blackBoard = GetComponent<AIController>().GetBlackBoardRef();
        foreach (var sensor in sensors)
        {
            sensor.Initialize(blackBoard);
        }
    }

    private void Update()
    {
        foreach (var sensor in sensors)
        {
            sensor.UpdateSensor(Time.deltaTime);
        }
    }

    private void InitializeSensorList()
    {
        sensors = new List<ISensor>();
        sensors.Add(new VisionSensor());
    }
}
