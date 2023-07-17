using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
    public Animator caveDoor;

    public bool puzzleComplete = false;


    private void Update()
    {
        if (gameObject.GetComponent<Puzzle>().puzzleControl)
        {


            if (!GameMenuManager.Instance.isPuzzleComplete)
            {
                Debug.Log("calosýyo");
                bool puzzleComplete = true;

                Transform[] parts = new Transform[]
                {
                part1.GetComponent<Collider>().transform,
                part2.GetComponent<Collider>().transform,
                part3.GetComponent<Collider>().transform,
                part4.GetComponent<Collider>().transform,
                part5.GetComponent<Collider>().transform,
                part6.GetComponent<Collider>().transform,
                part7.GetComponent<Collider>().transform,
                part8.GetComponent<Collider>().transform,
                part9.GetComponent<Collider>().transform,
                part10.GetComponent<Collider>().transform,
                part11.GetComponent<Collider>().transform,
                part12.GetComponent<Collider>().transform,
                part13.GetComponent<Collider>().transform,
                part14.GetComponent<Collider>().transform,
                part15.GetComponent<Collider>().transform,
                part16.GetComponent<Collider>().transform
                };

                foreach (Transform part in parts)
                {
                    if (part.eulerAngles.y > 0)
                    {
                        puzzleComplete = false;
                        break;
                    }
                }

                if (puzzleComplete)
                {
                    foreach (GameObject cube in new GameObject[] {
            cube1, cube2, cube3, cube4, cube5, cube6, cube7, cube8,
            cube9, cube10, cube11, cube12, cube13, cube14, cube15, cube16
        })
                    {
                        cube.transform.position -= new Vector3(0, 0.1f, 0);
                        GameMenuManager.Instance.isPuzzleComplete = true;
                        Cursor.lockState = CursorLockMode.Locked;

                        gameObject.GetComponent<Puzzle>().PuzzleExit();
                        caveDoor.SetTrigger("start");
                    }
                }
            }
        }
    }


}
