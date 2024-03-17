using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Animation_Event_helper : MonoBehaviour
{
    public UnityEvent OnAnimation_EventTriggered, On_Attack_performed;

    public void Trigger_Event()
    {
        OnAnimation_EventTriggered?.Invoke();
    }

    public void Trigger_Attack() { On_Attack_performed?.Invoke(); }
}
