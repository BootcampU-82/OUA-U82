using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCursorControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // ışın bir nesne ile çarpışırsa
            if (Physics.Raycast(ray, out hit, 100f))
            {
                // Işının "Enemy" etiketine sahip bir nesneyle çarpıştığı durumda
                if (hit.collider.CompareTag("PuzzlePart"))
                {
                    hit.collider.transform.Rotate(0, 0, 90);
                    
                }
            }
        }
        
    }
}
