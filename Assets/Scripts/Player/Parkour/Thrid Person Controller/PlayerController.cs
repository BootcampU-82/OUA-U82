using Assets.Scripts.Player.Weapon;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AimController aimController;

    [SerializeField] float rotationSpeed = 500f;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float walkSpeed=3f;
    [SerializeField] float sprintSpeed = 5f;
    float moveSpeed = 3f;

    /// <summary>
    /// Zemin Kontrol Yarýçapý
    /// </summary>
    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    /// <summary>
    /// Zemin Kontrol ofseti
    /// </summary>
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    /// <summary>
    /// Yerde mi?
    /// </summary>
    bool isGrounded;
    bool hasControl = true;

    public bool InAction { get; private set; }
    /// <summary>
    /// Asýlý mý?
    /// </summary>
    public bool IsHanging { get; set; }

    /// <summary>
    /// Ýstenilen Hareket Yönü
    /// </summary>
    Vector3 desiredMoveDir;
    /// <summary>
    /// Hareket yönü
    /// </summary>
    Vector3 moveDir;
    Vector3 velocity;

    /// <summary>
    /// Çýkýntýda mý?
    /// </summary>
    public bool IsOnLedge { get; set; }
    public LedgeData LedgeData { get; set; }

    float ySpeed;
    Quaternion targetRotation;

    CameraController cameraController; // Orijinal kod 
  
    Animator animator;
    CharacterController characterController;
    EnvironmentScanner environmentScanner;



    public float jumpForce = 5f; // Zýplama kuvveti
    public float gravity = 20f; // Yer çekimi katsayýsý
    private Vector3 moveDirection;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>(); //orijinal kod
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        environmentScanner = GetComponent<EnvironmentScanner>();
    }

    private void Update()
    {


        StateControl();
        // inputlar alýnýr
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // input normalizasyon iþlemi ile hareket miktarý bulunur
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        // input normalizasyonu
        var moveInput = (new Vector3(h, 0, v)).normalized;

        desiredMoveDir = cameraController.PlanarRotation * moveInput;
        moveDir = desiredMoveDir;

        if (!hasControl) return;

        if (IsHanging) return;

        velocity = Vector3.zero;

        GroundCheck();
        animator.SetBool("isGrounded", isGrounded);
        velocity = desiredMoveDir * moveSpeed;
        if (isGrounded)
        {
            animator.SetBool("isJump", false);

            ySpeed = -0.5f;
            

            IsOnLedge = environmentScanner.ObstacleLedgeCheck(desiredMoveDir, out LedgeData ledgeData);
            if (IsOnLedge)
            {
                LedgeData = ledgeData;
                LedgeMovement();
            }
            animator.SetFloat("moveAmount", velocity.magnitude, 0.2f, Time.deltaTime); // orijinal kod:animator.SetFloat("moveAmount", velocity.magnitude / moveSpeed, 0.2f, Time.deltaTime);

            if (Input.GetButton("Jump") && !InAction)
            {
                var hitData = environmentScanner.ObstacleCheck();

                if (!hitData.heightHitFound && !hitData.forwardHitFound && !IsOnLedge)
                {
                    animator.SetBool("isJump", true);
                    ySpeed = jumpForce;
                }
            }

        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
            //velocity = transform.forward * moveSpeed / 2;
        }

        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        
        if (moveAmount > 0 && moveDir.magnitude > 0.2f)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        if(!aimController.isCameraAimAlign)
        {
            RotatePlayer(targetRotation);
        }
       
       

    }

    //karakteri döndür
    public void RotatePlayer(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void StateControl()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouched", true);
            moveSpeed = crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }  
        else
        {
            animator.SetBool("isCrouched", false);
            moveSpeed = walkSpeed;
        }

    }

    /// <summary>
    /// Karakterin zeminde olup olmadýðýný kontrol eder ve sonucu isGrounded deðiþkenine atar.
    /// </summary>
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    void LedgeMovement()
    {
        float signedAngle = Vector3.SignedAngle(LedgeData.surfaceHit.normal, desiredMoveDir, Vector3.up);
        float angle = Mathf.Abs(signedAngle);

        if (Vector3.Angle(desiredMoveDir, transform.forward) >= 80)
        {
            // Don't move, but rotate
            velocity = Vector3.zero;
            return;
        }

        if (angle < 60)
        {
            velocity = Vector3.zero;
            moveDir = Vector3.zero;
        }
        else if (angle < 90)
        {
            // Angle is b/w 60 and 90, so limit the velocity to horizontal direction

            var left = Vector3.Cross(Vector3.up, LedgeData.surfaceHit.normal);
            var dir = left * Mathf.Sign(signedAngle);

            velocity = velocity.magnitude * dir;
            moveDir = dir;
        }
    }


    /// <summary>
    /// Eylem yap: 
    /// </summary>
    /// <param name="animName"></param>
    /// <param name="matchParams"></param>
    /// <param name="targetRotation"></param>
    /// <param name="rotate"></param>
    /// <param name="postDelay"></param>
    /// <param name="mirror"></param>
    /// <returns></returns>
    public IEnumerator DoAction(string animName, MatchTargetParams matchParams, Quaternion targetRotation, bool rotate=false, float postDelay=0f, bool mirror=false)
    {
        InAction = true;

        animator.SetBool("mirrorAction", mirror);
        // Belirtilen animasyona 0.2 saniye sonra geçiþ yap
        animator.CrossFadeInFixedTime(animName, 0.2f);
        // 1 frame kadar bekle ve sonra kaldýðýn yerden devam et
        yield return null;
        // Sonraki animasyon bilgisini alýr
        var animState = animator.GetNextAnimatorStateInfo(0);
        // Sonraki animasyon ayarlanmadýysa hata mesajý ver
        if (!animState.IsName(animName))
            Debug.LogError("The parkour animation is wrong!");

        float rotateStartTime = (matchParams != null) ? matchParams.startTime : 0f;

        float timer = 0f;
        //Animasyon uzunluðu kadar çalýþ
        while (timer <= animState.length)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / animState.length;

            if (rotate && normalizedTime > rotateStartTime)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (matchParams != null)
                MatchTarget(matchParams);

            if (animator.IsInTransition(0) && timer > 0.5f)
                break;

            yield return null;
        }

        yield return new WaitForSeconds(postDelay);

        InAction = false;
    }

    void MatchTarget(MatchTargetParams mp)
    {
        if (animator.isMatchingTarget) return;

        animator.MatchTarget(mp.pos, transform.rotation, mp.bodyPart, new MatchTargetWeightMask(mp.posWeight, 0),
            mp.startTime, mp.targetTime);
    }

    public void SetControl(bool hasControl)
    {
        this.hasControl = hasControl;
        characterController.enabled = hasControl;

        if (!hasControl)
        {
            animator.SetFloat("moveAmount", 0f);
            targetRotation = transform.rotation;
        }
    }

    public bool HasControl {
        get => hasControl;
        set => hasControl = value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    public float RotationSpeed => rotationSpeed;
}

/// <summary>
/// Hedef Parametreleri Eþleþtir
/// </summary>
public class MatchTargetParams
{
    public Vector3 pos;
    public AvatarTarget bodyPart;
    //konum Aðýrlýk
    public Vector3 posWeight;
    public float startTime;
    public float targetTime;
}
