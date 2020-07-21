using UnityEngine;

public abstract class Subscriber : MonoBehaviour
{
    public abstract void OnEvent(CustomEvent customEvent);

    private void OnDestroy()
    {
        EventChannel.Unsubscribe(this);
    }
}
