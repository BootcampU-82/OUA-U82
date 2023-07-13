using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleComplete : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;
    public GameObject part6;
    public GameObject part7;
    public GameObject part8;
    public GameObject part9;
    public GameObject part10;
    public GameObject part11;
    public GameObject part12;
    public GameObject part13;
    public GameObject part14;
    public GameObject part15;
    public GameObject part16;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public GameObject cube9;
    public GameObject cube10;
    public GameObject cube11;
    public GameObject cube12;
    public GameObject cube13;
    public GameObject cube14;
    public GameObject cube15;
    public GameObject cube16;
    
    public bool puzzleComplete = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            if (part1.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                if (part2.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                    if (part3.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                        if (part4.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                            if (part5.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                                if (part6.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                                    if (part7.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                                        if (part8.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                                            if (part9.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                                                if (part10.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                                                    if (part11.GetComponent<Collider>().transform.eulerAngles.y <= 0)
                                                        if (part12.GetComponent<Collider>().transform.eulerAngles.y <=
                                                            0)
                                                            if (part13.GetComponent<Collider>().transform.eulerAngles
                                                                    .y <= 0)
                                                                if (part14.GetComponent<Collider>().transform
                                                                        .eulerAngles.y <= 0)
                                                                    if (part15.GetComponent<Collider>().transform
                                                                            .eulerAngles.y <= 0)
                                                                        if (part16.GetComponent<Collider>().transform
                                                                                .eulerAngles.y <= 0)
                                                                        {
                                                                            puzzleComplete = true;
                                                                            cube1.transform.position =
                                                                                new Vector3(cube1.transform.position.x,
                                                                                    cube1.transform.position.y-0.1f,
                                                                                    cube1.transform.position.z);
                                                                            cube2.transform.position =
                                                                                new Vector3(cube2.transform.position.x,
                                                                                    cube2.transform.position.y-0.1f,
                                                                                    cube2.transform.position.z);
                                                                            cube3.transform.position =
                                                                                new Vector3(cube3.transform.position.x,
                                                                                    cube3.transform.position.y-0.1f,
                                                                                    cube3.transform.position.z);
                                                                            cube4.transform.position =
                                                                                new Vector3(cube4.transform.position.x,
                                                                                    cube4.transform.position.y-0.1f,
                                                                                    cube4.transform.position.z);
                                                                            cube5.transform.position =
                                                                                new Vector3(cube5.transform.position.x,
                                                                                    cube5.transform.position.y-0.1f,
                                                                                    cube5.transform.position.z);
                                                                            cube6.transform.position =
                                                                                new Vector3(cube6.transform.position.x,
                                                                                    cube6.transform.position.y-0.1f,
                                                                                    cube6.transform.position.z);
                                                                            cube7.transform.position =
                                                                                new Vector3(cube7.transform.position.x,
                                                                                    cube7.transform.position.y-0.1f,
                                                                                    cube7.transform.position.z);
                                                                            cube8.transform.position =
                                                                                new Vector3(cube8.transform.position.x,
                                                                                    cube8.transform.position.y-0.1f,
                                                                                    cube8.transform.position.z);
                                                                            cube9.transform.position =
                                                                                new Vector3(cube9.transform.position.x,
                                                                                    cube9.transform.position.y-0.1f,
                                                                                    cube9.transform.position.z);
                                                                            cube10.transform.position =
                                                                                new Vector3(cube10.transform.position.x,
                                                                                    cube10.transform.position.y-0.1f,
                                                                                    cube10.transform.position.z);
                                                                            cube11.transform.position =
                                                                                new Vector3(cube11.transform.position.x,
                                                                                    cube11.transform.position.y-0.1f,
                                                                                    cube11.transform.position.z);
                                                                            cube12.transform.position =
                                                                                new Vector3(cube12.transform.position.x,
                                                                                    cube12.transform.position.y-0.1f,
                                                                                    cube12.transform.position.z);
                                                                            cube13.transform.position =
                                                                                new Vector3(cube13.transform.position.x,
                                                                                    cube13.transform.position.y-0.1f,
                                                                                    cube13.transform.position.z);
                                                                            cube14.transform.position =
                                                                                new Vector3(cube14.transform.position.x,
                                                                                    cube14.transform.position.y-0.1f,
                                                                                    cube14.transform.position.z);
                                                                            cube15.transform.position =
                                                                                new Vector3(cube15.transform.position.x,
                                                                                    cube15.transform.position.y-0.1f,
                                                                                    cube15.transform.position.z);
                                                                            cube16.transform.position =
                                                                                new Vector3(cube16.transform.position.x,
                                                                                    cube16.transform.position.y-0.1f,
                                                                                    cube16.transform.position.z);
                                                                        }





        
    }
}
