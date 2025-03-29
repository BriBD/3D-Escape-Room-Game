
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerPickup : MonoBehaviour
{
    public Camera MainCamera;
    public Transform holdingPosition; // Position where the object will be held
    private Transform heldObject; // The currently held object
    private int pickupStrikeLeft = 0; // Tracks if something is picked up with the left hand
    private int pickupStrikeRight = 0; // Reserved for possible right-hand logic
    private bool isHandcuffed = false; //player is handcuffed

    public TMP_Text handcuffSlotText1; // text For handcuff key box
    public TMP_Text handcuffSlotText2;
    public TMP_Text handcuffSlotText3;
    private int currentNumber1; //holds number for handcuff box
    private int currentNumber2;
    private int currentNumber3;
    public Animator cuffBoxAnim;
    public Animator keyUpAnim;
    private List<int> availableNumbers = new List<int>();
    bool done = false;

    public float throwForce = 500f;



    void Start()
        { //numbers for the hancuff slot box
        
            currentNumber1 = 0;   
            currentNumber2 = 0;
            currentNumber3 = 0;
            UpdateText();


    //makes cursor visible and locks it in the middle of the screen
Cursor.visible = true;
Cursor.lockState = CursorLockMode.Confined;




        }
    void Update()
    {

        if (Input.GetMouseButtonDown(1) && heldObject != null) // Right mouse button
        {
            ThrowItem();
        }

if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Unlock the cursor and make it visible and free
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Lock the cursor and hide it
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
////

            // Handle picking up an object
        if (Input.GetMouseButtonDown(0)) // if left-click
            {
            if (heldObject == null) //if player is holding NOTHING, try pick up script
                {
                    TryPickup();
                }
                else
                { 
                    Debug.Log("Already holding an object in this hand!");
                }
            }


        if (Input.GetKeyDown(KeyCode.Q) && heldObject != null) //if "Q" is pressed and an object is held, drop object
        {
            DropItem(); // 
        }
        else if (Input.GetKeyDown(KeyCode.Q) && pickupStrikeRight == 0) //if  Q is pressed and not holding anything in right hand
        {
            Debug.Log($"I'm not holding anything");

        }

        //if left mouse button is clicked, the slot number on the box will change
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
               
                if (hit.collider.CompareTag("slotNumber")) // Check if the hit object is tagged as "slotNumber"
                {
                   
                    if (hit.transform == handcuffSlotText1.transform) 
                    {
                        ChangeSlotNumber(handcuffSlotText1, ref currentNumber1);
                        Debug.Log("touch");
                    }
                    else if (hit.transform == handcuffSlotText2.transform)
                    {
                        ChangeSlotNumber(handcuffSlotText2, ref currentNumber2);
                        Debug.Log("touch");
                    }
                    else if (hit.transform == handcuffSlotText3.transform)
                    {
                        ChangeSlotNumber(handcuffSlotText3, ref currentNumber3);
                        Debug.Log("touch");
                    }
                } 
                else if (hit.collider.gameObject.name == "heartKey")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }

    }

    void TryPickup()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Pickup")) // Check if item has pickup tag
            {   
                if (isHandcuffed == false) { //if not handcuffed, player can hold object
                    PickupObject(hit.collider.transform);
                    pickupStrikeLeft = 1; // Indicates an object is being held
                    Debug.Log($"Picking up: {hit.collider.name}...");
                    Debug.Log($"Pick up strike = 1");
                }
                
                if (isHandcuffed == true){
                    Debug.Log("Can't pick up anything when I'm handcuffed");
                }
            }
            else
            {
                Debug.Log("Object is not tagged as 'Pickup'.");
            }
        }
        else
        {
            Debug.Log("No object hit by the raycast.");
        }
    }


    private void PickupObject(Transform pickedObject)
    {
        // Disable gravity and make the object kinematic
        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true; // Prevent physics from interfering
        }

        // Attach the object to the holding position
        heldObject = pickedObject; // Set the held object reference
        heldObject.SetParent(holdingPosition);
        heldObject.localPosition = Vector3.zero; // Center it at the holding position
        heldObject.localRotation = Quaternion.identity; // Reset rotation

        Debug.Log($"Object '{pickedObject.name}' successfully picked up.");
    }

    private void DropItem()
    {
        // Detach the object from the holding position
        heldObject.SetParent(null);

        // Re-enable physics
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }


        // Clear the reference to the held object
        Debug.Log($"Dropping '{heldObject.name}'...");
        Debug.Log($"Picking up, strike = 0");
        heldObject = null;
        pickupStrikeLeft = 0; // Reset pickup status
    }


public void ChangeSlotNumber(TMP_Text handcuffSlotText, ref int currentNumber)
{
    currentNumber = Random.Range(0,10);
    handcuffSlotText.text = currentNumber.ToString();

    int cuffBoxNumber1 = int.Parse(handcuffSlotText1.text);
    int cuffBoxNumber2 = int.Parse(handcuffSlotText2.text);
    int cuffBoxNumber3 = int.Parse(handcuffSlotText3.text);  

    if (cuffBoxNumber1 == 4 && cuffBoxNumber2 == 4 && cuffBoxNumber3 == 0 && !done)
    {
        isHandcuffed = false;
        Debug.Log("Handcuffs unlocked and box opened!");
        cuffBoxAnim.SetTrigger("Open");
        keyUpAnim.SetTrigger("keyUp");
        done = true;
    }
}

    

    public void UpdateText()
    {
        // handcuffSlotText.text = currentNumber.ToString();
        handcuffSlotText1.text = currentNumber1.ToString();
        handcuffSlotText2.text = currentNumber2.ToString();
        handcuffSlotText3.text = currentNumber3.ToString();
    }

   private void ThrowItem()
{
    // Detach the object from the player
    heldObject.SetParent(null);

    Rigidbody rb = heldObject.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        // Apply force forward from the camera
        rb.AddForce(MainCamera.transform.forward * throwForce);
    }

    Debug.Log($"Threw '{heldObject.name}'");

    // Clear the reference
    heldObject = null;
    pickupStrikeLeft = 0;
}





}