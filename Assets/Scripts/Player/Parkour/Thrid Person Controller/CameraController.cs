using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    /// <summary>
    /// Takip edilecek hedef
    /// </summary>
    public Transform followTarget;

    [SerializeField] float rotationSpeed = 2f;
    /// <summary>
    /// Kameranýn hedefe olan uzaklýðý (bu deðer x,y ve z uzaklýðýný ayný anda temsil eder)
    /// </summary>
    public float distance = 5;

    /// <summary>
    /// Minimum Dikey Açý
    /// </summary>
    [SerializeField] float minVerticalAngle = -45;
    /// <summary>
    /// Maksimum Dikey Açý
    /// </summary>
    [SerializeField] float maxVerticalAngle = 45;

    /// <summary>
    /// Çerçeve ofseti. Kameranýn takip ettiði objeye göre x ve y'deki konumu
    /// </summary>
    [SerializeField] Vector2 framingOffset;

    /// <summary>
    /// X ekseni tersine çevrilsin mi?
    /// </summary>
    [SerializeField] bool invertX;
    /// <summary>
    /// Y ekseni tersine çevrilsin mi?
    /// </summary>
    [SerializeField] bool invertY;


    float rotationX;
    float rotationY;

    /// <summary>
    /// X ekseni ters çevirme deðeri
    /// </summary>
    float invertXVal;
    /// <summary>
    /// Y ekseni ters çevirme deðeri
    /// </summary>
    float invertYVal;

    private void Start()
    {
        UnShowMouseCursor();
        SetInvertAxes();
    }

    private void Update()
    {
        
        rotationX += Input.GetAxis("Camera Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Camera X") * invertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPostion = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = focusPostion - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    /// <summary>
    /// Kameranýn y ekseninde nereye baktýðýnýn rotasyon deðerini tutar
    /// </summary>
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);

    /// <summary>
    /// Fare imlecinin ekrandaki görünürlüðünü kapatýr.
    /// </summary>
    void UnShowMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Eksenleri ters çevirme ayarlanýr
    /// </summary>
    void SetInvertAxes()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;
    }

    /// <summary>
    /// Kamerayý hizalar
    /// </summary>
    /// <param name="followTargetPos"></param>
    /// <param name="distance"></param>
    public void CameraAlign(Vector3 followTargetPos, float distance)
    {
        followTarget.transform.localPosition = followTargetPos;
        this.distance = distance;
    }


}
