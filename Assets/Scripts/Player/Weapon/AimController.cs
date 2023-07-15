using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Assets.Scripts.Player.Weapon
{
    [RequireComponent(typeof(CameraController))]
    public class AimController : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;
        [SerializeField] BodyControl bodyControl;
        [SerializeField] Rig rigLayerHandIK;
        [SerializeField] GameObject weapon;


        CameraController cameraController;


        [SerializeField] Transform mainCamera, cameraWeaponPos;

        
        /// <summary>
        /// Camera aim olayı için hizalandı mı?
        /// </summary>
        internal bool isCameraAimAlign;

        public Transform weaponPoint;
        private void Awake()
        {
            cameraController = GetComponent<CameraController>();
        }

        private void Start()
        {
            rigLayerHandIK.weight = 0f;
            weapon.SetActive(false);



            cameraController.CameraAlign(new Vector3(0.75f, 0f, 0f), 1.5f);
            bodyControl.SetMACWeightsAiming(0.3f);
            rigLayerHandIK.weight = 1f;
            weapon.SetActive(true);
            isCameraAimAlign = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                EnemyHitControl();
            }

            if (isCameraAimAlign)
            {
                playerController.RotatePlayer(Quaternion.LookRotation(cameraController.PlanarRotation*new Vector3(0f,0f,1f)));
                
            }

            
                if (InputController() && !isCameraAimAlign)
                {
                    cameraController.CameraAlign(new Vector3(0.75f, 0f, 0f), 1.5f);
                    bodyControl.SetMACWeightsAiming(0.3f);
                    rigLayerHandIK.weight = 1f;
                    weapon.SetActive(true);
                    isCameraAimAlign = true;
                }
                else if (!InputController() && isCameraAimAlign)
                {
                    cameraController.CameraAlign(Vector3.zero, 2.5f);
                    bodyControl.SetMACWeightsAiming(0.05f);
                    rigLayerHandIK.weight = 0f;
                    weapon.SetActive(false);
                    isCameraAimAlign = false;
                }
            
            
            
            
            float rotValue;
            if (mainCamera.eulerAngles.x>180)
                rotValue = -20f - (mainCamera.eulerAngles.x - 360f);
            else
                rotValue = -20f - mainCamera.eulerAngles.x;

            cameraWeaponPos.localPosition = new Vector3(cameraWeaponPos.localPosition.x, cameraWeaponPos.localPosition.y, 2.3f - (Mathf.Abs(rotValue)  * 0.004f));
            weapon.transform.position = cameraWeaponPos.position;
        }



       


        /// <summary>
        /// Düşman vuruldu kontrolü: Düşman vuruldu mu vurulmadı mı diye kontrol eder. Vurulduysa düşmanın gameobject'ini döndürür
        /// </summary>
        /// <returns></returns>
        GameObject EnemyHitControl()
        {
            // Silahın ucundan ileri doğru bir ışın gönder
            if (Physics.Raycast(weaponPoint.position, weaponPoint.forward, out RaycastHit hit, 200f))
            {
                // Işının çarptığı nesne, "Enemy" etiketine sahip bir nesne ise
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Düşman vuruldu!");
                    return hit.transform.gameObject;
                }
                else
                    return null;
            }
            else return null;
        }


        /// <summary>
        /// Aim olayını gerçekleştiren input girildi mi?
        /// </summary>
        /// <returns></returns>
        bool InputController()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                return true;
            }
            else return false;
        }

    }
}