using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour
{

    private HingeJoint hinge;
    // Start is called before the first frame update
    void Start()
    {
       hinge = GetComponent<HingeJoint>();
       hinge.useMotor = false; //disables motor for door at the start so it doesnt move
    }

public void OpenDoor()
{
    Debug.Log("OpenDoor() was called");

    JointMotor motor = hinge.motor;
    motor.force = 100f;
    motor.targetVelocity = 100f;
    motor.freeSpin = false;

    hinge.motor = motor;
    hinge.useMotor = true;
}

    // Update is called once per frame

}
