using UnityEngine;


public class TimeInvoker : MonoBehaviour
{
    void Update()
    {
        float deltaTime = Time.deltaTime;
        EventBus.Invoke(new TimeUpdatedSignal(deltaTime));
    }
}
