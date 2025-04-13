using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsOpen : MonoBehaviour
{

    bool BR1DoorLocked = true; //door is unable to be opened/is locked

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if player right clicks
        {
            GameObject hit = RaycastManager.Instance.HitObject;
            if (hit != null)  //if player right clicks an object
                { 
                    if (hit.gameObject.name == ("heartKey")) //if player clicks heart key, door unlocks
                    {
                        BR1DoorLocked = false;
                        Debug.Log("Door unlocked!");
                    }
                    if (hit.gameObject.name == ("bedroom1ToHallway") && BR1DoorLocked == true) //if door is locked and door hit, door does not open
                    {
                    Debug.Log("Door did not budge");
                    } 
                    else if (hit.gameObject.name == ("bedroom1ToHallway") && BR1DoorLocked == false) //if door clicked and door unlocked, door opens
                    {
                        DoorHinge door = hit.transform.GetComponentInParent<DoorHinge>();
                        if (door != null){
                        door.OpenDoor();
                        Debug.Log("Opening door");
                    }
                    
                }
            } 
        }
    }
}
