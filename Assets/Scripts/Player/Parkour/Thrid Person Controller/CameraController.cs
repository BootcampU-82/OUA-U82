using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    /// <summary>
    /// Takip edilecek hedef
    /// </summary>
    public Transform followTarget;

    [SerializeField] float rotationSpeed = 2f;
    /// <summary>
    /// Kameran�n hedefe olan uzakl��� (bu de�er x,y ve z uzakl���n� ayn� anda temsil eder)
    /// </summary>
    public float distance = 5;

    /// <summary>
    /// Minimum Dikey A��
    /// </summary>
    [SerializeField] float minVerticalAngle = -45;
    /// <summary>
    /// Maksimum Dikey A��
    /// </summary>
    [SerializeField] float maxVerticalAngle = 45;

    /// <summary>
    /// �er�eve ofseti. Kameran�n takip etti�i objeye g�re x ve y'deki konumu
    /// </summary>
    [SerializeField] Vector2 framingOffset;

    /// <summary>
    /// X ekseni tersine �evrilsin mi?
    /// </summary>
    [SerializeField] bool invertX;
    /// <summary>
    /// Y ekseni tersine �evrilsin mi?
    /// </summary>
    [SerializeField] bool invertY;


    float rotationX;
    float rotationY;

    /// <summary>
    /// X ekseni ters �evirme de�eri
    /// </summary>
    float invertXVal;
    /// <summary>
    /// Y ekseni ters �evirme de�eri
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
    /// Kameran�n y ekseninde nereye bakt���n�n rotasyon de�erini tutar
    /// </summary>
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);

    /// <summary>
    /// Fare imlecinin ekrandaki g�r�n�rl���n� kapat�r.
    /// </summary>
    void UnShowMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Eksenleri ters �evirme ayarlan�r
    /// </summary>
    void SetInvertAxes()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;
    }

    /// <summary>
    /// Kameray� hizalar
    /// </summary>
    /// <param name="followTargetPos"></param>
    /// <param name="distance"></param>
    public void CameraAlign(Vector3 followTargetPos, float distance)
    {
        followTarget.transform.localPosition = followTargetPos;
        this.distance = distance;
    }


}
