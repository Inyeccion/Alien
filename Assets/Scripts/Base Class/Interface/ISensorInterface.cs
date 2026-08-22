using UnityEngine;

public interface ISensor
{
    void Initialize(BlackBoard blackBoard);

    void UpdateSensor(float deltaTime);

    void Clear();
}
