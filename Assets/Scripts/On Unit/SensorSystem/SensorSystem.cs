using UnityEngine;
using System.Collections.Generic;
public class SensorSystem : MonoBehaviour
{
    public BlackBoard blackBoard;

    private List<ISensor> sensors;

    private void Update()
    {
        foreach (var sensor in sensors)
        {
            sensor.UpdateSensor(Time.deltaTime);
        }
    }

    public void Initialize(BlackBoard board)
    {
        blackBoard = board; 
    }
}
