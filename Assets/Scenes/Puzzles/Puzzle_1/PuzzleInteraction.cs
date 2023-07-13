using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject puzzle;
    public GameObject character;
    public GameObject puzzleCameraPoint;
    public GameObject crossHair;
    public bool puzzleControl = false;
    private void Update()
    {
        
        // Puzzle a olan mesafe ölçülür
        float distance = Vector3.Distance (puzzle.transform.position, character.transform.position);
        // Eğer puzzle var ise ve 2 birimden daha yakınsa
        if (PuzzleDetection() && distance<2)
            if (Input.GetKeyDown(KeyCode.E) && puzzleControl==false)
            {
                puzzleControl = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                crossHair.GetComponent<Image>().enabled = false;
                mainCamera.GetComponent<CameraController>().enabled = false;
                character.GetComponent<PlayerController>().enabled = false;
                character.GetComponent<Animator>().enabled = false;
                mainCamera.transform.position = puzzleCameraPoint.transform.position;
                mainCamera.transform.rotation = Quaternion.Euler(75, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.X) && puzzleControl==true)
            {
                puzzleControl = false;
                Cursor.lockState = CursorLockMode.Locked;
                crossHair.GetComponent<Image>().enabled = true;
                mainCamera.GetComponent<CameraController>().enabled = true;
                character.GetComponent<PlayerController>().enabled = true;
                character.GetComponent<Animator>().enabled = true;

            }
            
                
    }

    /// <summary>
    /// Crosshair'in baktığı noktada puzzle var mı yokmu diye kontrol eder
    /// </summary>
    /// <returns></returns>
    bool PuzzleDetection()
    {
        // Ekran ortasından kameranın baktığı yöne doğru bir ışın gönder
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        RaycastHit hit;
        // ışın bir nesne ile çarpışırsa
        if (Physics.Raycast(ray, out hit, 200f))
        {
            // Işının "Puzzle" etiketine sahip bir nesneyle çarpıştığı durumda
            if (hit.collider.CompareTag("Puzzle"))
            {
                return true;
            }
            else
                return false;
        }
        // ışın bir nesne ile çarpışmazsa
        else
            return false;
    }

}

