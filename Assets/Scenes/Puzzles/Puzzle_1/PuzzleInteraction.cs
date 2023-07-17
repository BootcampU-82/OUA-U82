using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject puzzle;
    public GameObject character;
    public GameObject puzzleCameraPoint;
    public bool puzzleControl = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && puzzleControl == false)
            {
                PuzzleEnter();
            }
            if (Input.GetKeyDown(KeyCode.X) && puzzleControl == true)
            {
                PuzzleExit();
            }
        }

    }

    public void PuzzleExit()
    {
        puzzleControl = false;
        Cursor.lockState = CursorLockMode.Locked;

        mainCamera.GetComponent<CameraController>().enabled = true;
        character.GetComponent<PlayerController>().enabled = true;
        character.GetComponent<Animator>().enabled = true;
    }

    public void PuzzleEnter()
    {
        puzzleControl = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        mainCamera.GetComponent<CameraController>().enabled = false;
        character.GetComponent<PlayerController>().enabled = false;
        character.GetComponent<Animator>().enabled = false;
        mainCamera.transform.position = puzzleCameraPoint.transform.position;
        mainCamera.transform.rotation = Quaternion.Euler(75, 0, 0);
    }
  
}

