using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentControl : MonoBehaviour
{
    public List<Component> components = new List<Component>();

    private void Update()
    {
        if (RecordPlayer.replay_state != Replay_State.Recording && RecordPlayer.replay_state != Replay_State.none)
        {
            foreach(Component component in components)
            {
                if(component is Rigidbody)
                {
                    (component as Rigidbody).isKinematic = true;
                }
                else if(component is Behaviour)
                {
                    (component as Behaviour).enabled = false;
                }
                else if(component is Collider)
                {
                    (component as Collider).enabled = false;
                }
            }
        }
        else
        {
            foreach (Component component in components)
            {
                if (component is Rigidbody)
                {
                    (component as Rigidbody).isKinematic = false;
                }
                else if (component is Behaviour)
                {
                    (component as Behaviour).enabled = true;
                }
                else if (component is Collider)
                {
                    (component as Collider).enabled = true;
                }
            }
        }
    }
}
