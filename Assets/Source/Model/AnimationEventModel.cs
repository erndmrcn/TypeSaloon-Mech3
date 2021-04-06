using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventModel : MonoBehaviour
{

    public EventModel OnTrig;

    public void Trigger()
    {
        OnTrig.Invoke();
    }
}

[System.Serializable]
public class EventModel : UnityEngine.Events.UnityEvent { }
