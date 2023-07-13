using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotateSword : MonoBehaviour
{
    public int speed;
    public GameObject sword1;
    public GameObject sword2;
    public GameObject sword3;
    public GameObject sword4;
    public GameObject sword5;
    public GameObject sword6;
    public GameObject sword8;
    public GameObject sword7;
    public GameObject sword9;

    
    
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        sword1.transform.Rotate(0, -speed * Time.deltaTime, 0);
        sword2.transform.Rotate(0, speed * Time.deltaTime, 0);
        sword3.transform.Rotate(0, -speed * Time.deltaTime, 0);
        sword4.transform.Rotate(0, speed * Time.deltaTime, 0);
        sword5.transform.Rotate(0, -speed * Time.deltaTime, 0);
        sword6.transform.Rotate(0, speed * Time.deltaTime, 0);
        sword7.transform.Rotate(0, -speed * Time.deltaTime, 0);
        sword8.transform.Rotate(0, speed * Time.deltaTime, 0);
        sword9.transform.Rotate(0, -speed * Time.deltaTime, 0);
        
        
    }
}
