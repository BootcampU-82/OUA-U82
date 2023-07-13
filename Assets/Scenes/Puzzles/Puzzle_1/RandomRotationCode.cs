using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        int angle = Random.Range(0, 3);
        switch (angle)
        { 
            case 0:
                transform.Rotate(0, 0, 90);
                break;
            case 1:
                transform.Rotate(0, 0, 180);
                break;
            case 2:
                transform.Rotate(0, 0, 270);
                break;
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
