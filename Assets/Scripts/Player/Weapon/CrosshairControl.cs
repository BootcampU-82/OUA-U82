using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player.Weapon
{
    /// <summary>
    /// Crosshair ile ilgili işlemlerden sorumlu
    /// </summary>
    public class CrosshairControl : MonoBehaviour
    {

        public Camera mainCamera;
        public Image crosshair;

        private void Update()
        {
            
            if(EnemyDetection())
                crosshair.color = Color.red;
            else
                crosshair.color = Color.white;
        }

        /// <summary>
        /// Crosshair'in baktığı noktada düşman var mı yokmu diye kontrol eder
        /// </summary>
        /// <returns></returns>
        bool EnemyDetection()
        {
            // Ekran ortasından kameranın baktığı yöne doğru bir ışın gönder
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            RaycastHit hit;
            // ışın bir nesne ile çarpışırsa
            if (Physics.Raycast(ray, out hit, 200f))
            {
                // Işının "Enemy" etiketine sahip bir nesneyle çarpıştığı durumda
                if (hit.collider.CompareTag("Enemy"))
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
}