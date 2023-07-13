using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {

    public int speed;

    // Update is called once per frame
    void Update()
    {
        if (90 > this.transform.eulerAngles.y)
        {
            this.transform.Rotate(0, speed * Time.deltaTime, 0);
        }

    }
}
