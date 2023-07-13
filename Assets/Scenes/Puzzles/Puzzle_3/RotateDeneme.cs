using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDeneme : MonoBehaviour
{
    public GameObject pivotPoint;

    public float rotationSpeed;
    public bool press = false;
    // Start is called before the first frame update
    void Start()
    {

        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            press = true;  
        }
            
        if (transform.eulerAngles.z < 90 && press == true)
        {
            print(transform.rotation);
            transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);    
        }
        
        else if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 91)
        {
            transform.eulerAngles = new Vector3(0,0,0);
            press = false;
        }


    }
}
