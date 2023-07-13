using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFail : MonoBehaviour
{
   
    // Start is called before the first frame update

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "PuzzleFail")
        {
            print("PuzzleFail");
        }
            
    }
}
