using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSword : MonoBehaviour
{

    public int speed;
    public bool Rotate90_1 = false;
    public bool Rotate180_1 = false;
    public bool Rotate270_1 = false;
    public bool Rotate360_1 = false;
    public bool Rotate90_2 = false;
    public bool Rotate180_2 = false;
    public bool Rotate270_2 = false;
    public bool Rotate360_2 = false;
    public bool Rotate90_3 = false;
    public bool Rotate180_3 = false;
    public bool Rotate270_3 = false;
    public bool Rotate360_3 = false;
    public bool Rotate90_4 = false;
    public bool Rotate180_4 = false;
    public bool Rotate270_4 = false;
    public bool Rotate360_4 = false;
    public bool Rotate90_5 = false;
    public bool Rotate180_5 = false;
    public bool Rotate270_5 = false;
    public bool Rotate360_5 = false;
    public bool Rotate90_6 = false;
    public bool Rotate180_6 = false;
    public bool Rotate270_6 = false;
    public bool Rotate360_6 = false;
    public bool Rotate90_7 = false;
    public bool Rotate180_7 = false;
    public bool Rotate270_7 = false;
    public bool Rotate360_7 = false;
    public bool Rotate90_8 = false;
    public bool Rotate180_8 = false;
    public bool Rotate270_8 = false;
    public bool Rotate360_8 = false;
    public bool Rotate90_9 = false;
    public bool Rotate180_9 = false;
    public bool Rotate270_9 = false;
    public bool Rotate360_9 = false;
    
    
    public GameObject sword1;
    public GameObject sword2;
    public GameObject sword3;
    public GameObject sword4;
    public GameObject sword5;
    public GameObject sword6;
    public GameObject sword8;
    public GameObject sword7;
    public GameObject sword9;
    
    
    
    
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Rotate90_1")
        {
            Rotate90_1 = true;
        }
        if (col.gameObject.tag == "Rotate180_1")
        {
            Rotate180_1 = true;
        }
        if (col.gameObject.tag == "Rotate270_1")
        {
            Rotate270_1 = true;
        }
        if (col.gameObject.tag == "Rotate360_1")
        {
            Rotate360_1 = true;
        }
        if (col.gameObject.tag == "Rotate90_2")
        {
            Rotate90_2 = true;
        }
        if (col.gameObject.tag == "Rotate180_2")
        {
            Rotate180_2 = true;
        }
        if (col.gameObject.tag == "Rotate270_2")
        {
            Rotate270_2 = true;
        }
        if (col.gameObject.tag == "Rotate360_2")
        {
            Rotate360_2 = true;
        }
        if (col.gameObject.tag == "Rotate90_3")
        {
            Rotate90_3 = true;
        }
        if (col.gameObject.tag == "Rotate180_3")
        {
            Rotate180_3 = true;
        }
        if (col.gameObject.tag == "Rotate270_3")
        {
            Rotate270_3 = true;
        }
        if (col.gameObject.tag == "Rotate360_3")
        {
            Rotate360_3 = true;
        }
        if (col.gameObject.tag == "Rotate90_4")
        {
            Rotate90_4 = true;
        }
        if (col.gameObject.tag == "Rotate180_4")
        {
            Rotate180_4 = true;
        }
        if (col.gameObject.tag == "Rotate270_4")
        {
            Rotate270_4 = true;
        }
        if (col.gameObject.tag == "Rotate360_4")
        {
            Rotate360_4 = true;
        }
        if (col.gameObject.tag == "Rotate90_5")
        {
            Rotate90_5 = true;
        }
        if (col.gameObject.tag == "Rotate180_5")
        {
            Rotate180_5 = true;
        }
        if (col.gameObject.tag == "Rotate270_5")
        {
            Rotate270_5 = true;
        }
        if (col.gameObject.tag == "Rotate360_5")
        {
            Rotate360_5 = true;
        }
        if (col.gameObject.tag == "Rotate90_6")
        {
            Rotate90_6 = true;
        }
        if (col.gameObject.tag == "Rotate180_6")
        {
            Rotate180_6 = true;
        }
        if (col.gameObject.tag == "Rotate270_6")
        {
            Rotate270_6 = true;
        }
        if (col.gameObject.tag == "Rotate360_6")
        {
            Rotate360_6 = true;
        }
        if (col.gameObject.tag == "Rotate90_1")
        {
            Rotate90_7 = true;
        }
        if (col.gameObject.tag == "Rotate180_7")
        {
            Rotate180_7 = true;
        }
        if (col.gameObject.tag == "Rotate270_7")
        {
            Rotate270_7 = true;
        }
        if (col.gameObject.tag == "Rotate360_7")
        {
            Rotate360_7 = true;
        }
        if (col.gameObject.tag == "Rotate90_8")
        {
            Rotate90_8 = true;
        }
        if (col.gameObject.tag == "Rotate180_8")
        {
            Rotate180_8 = true;
        }
        if (col.gameObject.tag == "Rotate270_8")
        {
            Rotate270_8 = true;
        }
        if (col.gameObject.tag == "Rotate360_8")
        {
            Rotate360_8 = true;
        }
        if (col.gameObject.tag == "Rotate90_9")
        {
            Rotate90_9 = true;
        }
        if (col.gameObject.tag == "Rotate180_9")
        {
            Rotate180_9 = true;
        }
        if (col.gameObject.tag == "Rotate270_9")
        {
            Rotate270_9 = true;
        }
        if (col.gameObject.tag == "Rotate360_9")
        {
            Rotate360_9 = true;
        }
    }

    void Update()
    {
        if (90 > sword1.transform.eulerAngles.y && Rotate90_1 == true)
        {
            print(sword1.transform.eulerAngles.y);
            sword1.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate90_1 = false;
        }
        if (180 > sword1.transform.eulerAngles.y && Rotate180_1 == true)
        {
            print(sword1.transform.eulerAngles.y);
            sword1.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate180_1 = false;
        }
        if (270 > sword1.transform.eulerAngles.y && Rotate270_1 == true)
        {
            print(sword1.transform.eulerAngles.y);
            sword1.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate270_1 = false;
        }
        if (360 > sword1.transform.eulerAngles.y && Rotate360_1 == true)
        {
            print(sword1.transform.eulerAngles.y);
            sword1.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate360_1 = false;
        }
        if (90 > sword2.transform.eulerAngles.y && Rotate90_2 == true)
        {
            print(sword2.transform.eulerAngles.y);
            sword2.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate90_2 = false;
        }
        if (180 > sword2.transform.eulerAngles.y && Rotate180_2 == true)
        {
            print(sword2.transform.eulerAngles.y);
            sword2.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate180_2 = false;
        }
        if (270 > sword2.transform.eulerAngles.y && Rotate270_2 == true)
        {
            print(sword2.transform.eulerAngles.y);
            sword2.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate270_2 = false;
        }
        if (360 > sword2.transform.eulerAngles.y && Rotate360_2 == true)
        {
            print(sword2.transform.eulerAngles.y);
            sword2.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate360_2 = false;
        }
        if (90 > sword3.transform.eulerAngles.y && Rotate90_3 == true)
        {
            print(sword3.transform.eulerAngles.y);
            sword3.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate90_3 = false;
        }
        if (180 > sword3.transform.eulerAngles.y && Rotate180_3 == true)
        {
            print(sword3.transform.eulerAngles.y);
            sword3.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate180_3 = false;
        }
        if (270 > sword3.transform.eulerAngles.y && Rotate270_3 == true)
        {
            print(sword3.transform.eulerAngles.y);
            sword3.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate270_3 = false;
        }
        if (360 > sword3.transform.eulerAngles.y && Rotate360_1 == true)
        {
            print(sword3.transform.eulerAngles.y);
            sword3.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            Rotate360_3 = false;
        }
    }
}
