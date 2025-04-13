using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{


    public static RaycastManager Instance;

    public GameObject HitObject {get; private set;}
    public RaycastHit HitInfo {get; private set;}

    void Awake()
    {
       if (Instance == null) 
       {
        Instance = this;
       } 
       else  if (RaycastManager.Instance == null) 
       {
        Debug.LogError("RaycastManager.Instance is null");
       }
       else
    {
        Destroy(gameObject);
    }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                HitObject = hit.collider.gameObject;
                HitInfo = hit;
            }
            else
            {
                HitObject = null;
            }
            
        }
    }
} 