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
        [SerializeField] GameObject crosshair;


        CameraController cameraController;

        float enemyCount=0;

        [SerializeField] GameObject hintUI;
        [SerializeField] GameObject FinalMap;

        [SerializeField] Transform mainCamera, cameraWeaponPos;


        [SerializeField] GameObject poisonVfx;
        int bossCount = 0;


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
                    crosshair.SetActive(true);
                    cameraController.CameraAlign(new Vector3(0.75f, 0f, 0f), 1.5f);
                    bodyControl.SetMACWeightsAiming(0.3f);
                    rigLayerHandIK.weight = 1f;
                    weapon.SetActive(true);
                    isCameraAimAlign = true;
                }
                else if (!InputController() && isCameraAimAlign)
                {
                    crosshair.SetActive(false);
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

            // Create a ray from the camera's position to the screen center.
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            // Cast the ray.
            RaycastHit hit;
            // Silahın ucundan ileri doğru bir ışın gönder
            if (Physics.Raycast(ray, out hit))
            {
                // Işının çarptığı nesne, "Enemy" etiketine sahip bir nesne ise
                if (hit.collider.CompareTag("Enemy"))
                {
                    enemyCount++;
                    if (enemyCount ==3)
                    {
                        GameMenuManager.Instance.havePuzzleHint = true;
                        hintUI.SetActive(true);
                        Destroy(hintUI, 1f);
                    }

                    GameObject blood = Instantiate(poisonVfx, hit.transform);
                    Destroy(hit.collider.gameObject);
                    Destroy(blood, 0.3f);
                    
                    Debug.Log("Düşman vuruldu!");
                    return hit.transform.gameObject;
                }
                else if(hit.collider.CompareTag("EnemyBoss"))
                {
                    Debug.Log("Boss Vuruldu");
                    bossCount++;
                    if (bossCount > 10)
                    {
                        GameMenuManager.Instance.bossDead = true;
                        Destroy(hit.collider.gameObject);
                        FinalMap.SetActive(true);
                    }
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