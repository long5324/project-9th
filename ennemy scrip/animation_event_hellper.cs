using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class animation_event_hellper : MonoBehaviour
{
    public UnityEvent animtation_event;
    public UnityEvent animtation_event1;
    public void trigger_event()
    {
        animtation_event?.Invoke();
    }
    public void trigger_event_air_attack()
    {
        animtation_event1?.Invoke();
    }
}
