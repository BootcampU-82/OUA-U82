using System.Collections;
using UnityEngine;

/// <summary>
/// Týrmanýþ Denetleyicisi
/// </summary>
public class ClimbController : MonoBehaviour
{
    /// <summary>
    /// Þuanda üzerinde bulunduðum týrmanýþ noktasý
    /// </summary>
    ClimbPoint currentPoint;

    PlayerController playerController;
    EnvironmentScanner envScanner;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        envScanner = GetComponent<EnvironmentScanner>();
    }

    private void Update()
    {
        if (!playerController.IsHanging)
        {
            if (Input.GetButton("Jump") && !playerController.InAction)
            {
                if (envScanner.ClimbLedgeCheck(transform.forward, out RaycastHit ledgeHit))
                {
                    currentPoint = ledgeHit.transform.GetComponent<ClimbPoint>();

                    playerController.SetControl(false);
                    StartCoroutine(JumpToLedge("IdleToHang", ledgeHit.transform, 0.41f, 0.54f));
                }
            }
        }
        else
        {
            float h = Mathf.Round(Input.GetAxisRaw("Horizontal"));
            float v = Mathf.Round(Input.GetAxisRaw("Vertical"));
            var inputDir = new Vector2(h, v);

            // Karakter bir eylemde bulunuyorsa yani animasyon oynatýlýyorsa ya da herhangi bir tuþa basýlmadýysa dön
            if (playerController.InAction || inputDir == Vector2.zero && !Input.GetButton("Jump")) return;
            
            var neighbour = currentPoint.GetNeighbour(inputDir);
            // klavyeden tuþlara basýldýysa
            if(inputDir != Vector2.zero)
            {
                // Üst çýkýntýdaysam ve klavyeden yukarý tuþu ile zýplama tuþuna bastýysam
                if(currentPoint.transform.parent.CompareTag("UpperLedge") && inputDir == Vector2.up && Input.GetButton("Jump"))
                {
                    StartCoroutine(DoActionWithoutIK("BracedHangToCrouch"));
                }
                // tuþlarla belirtilen yönde komþu yoksa dön
                if (neighbour == null) return;

                if (neighbour.connectionType == ConnectionType.Jump && Input.GetButton("Jump"))
                {
                    currentPoint = neighbour.point;
                    JumpToNeighbour(neighbour);
                }
                else if (neighbour.connectionType == ConnectionType.Move)
                {
                    currentPoint = neighbour.point;
                    MoveToNeighbour(neighbour);
                }
            }
            
            
            else if (Input.GetButton("Jump") && !playerController.InAction)
            {
                StartCoroutine(DoActionWithoutIK("HangToIdle"));
                return;
            }
        }
    }

    /// <summary>
    /// Komþu çýkýntýya zýplar
    /// </summary>
    void JumpToNeighbour(Neighbour neighbour)
    {
        if (neighbour.direction.y == 1)
            StartCoroutine(JumpToLedge("HangHopUp", currentPoint.transform, 0.35f, 0.65f, handOffset: new Vector3(0.25f, 0.08f, 0.15f)));
        else if (neighbour.direction.y == -1)
            StartCoroutine(JumpToLedge("HangHopDown", currentPoint.transform, 0.31f, 0.65f, handOffset: new Vector3(0.25f, 0.1f, 0.13f)));
        else if (neighbour.direction.x == 1)
            StartCoroutine(JumpToLedge("HangHopRight", currentPoint.transform, 0.20f, 0.50f));
        else if (neighbour.direction.x == -1)
            StartCoroutine(JumpToLedge("HangHopLeft", currentPoint.transform, 0.20f, 0.50f));
    }

    /// <summary>
    /// Komþu noktaya hareket eder
    /// </summary>
    /// <param name="neighbour"></param>
    void MoveToNeighbour(Neighbour neighbour)
    {
        if (neighbour.direction.x == 1)
            StartCoroutine(JumpToLedge("ShimmyRight", currentPoint.transform, 0f, 0.38f, handOffset: new Vector3(0.25f, 0.05f, 0.1f)));
        else if (neighbour.direction.x == -1)
            StartCoroutine(JumpToLedge("ShimmyLeft", currentPoint.transform, 0f, 0.38f, AvatarTarget.LeftHand, handOffset: new Vector3(0.25f, 0.05f, 0.1f)));
    }

    /// <summary>
    /// IK(Prosedürel animasyon) olmadan eylem gerçekleþtir
    /// </summary>
    /// <param name="animName">Oynatýlacak animasyonun ismi</param>
    /// <returns></returns>
    IEnumerator DoActionWithoutIK(string animName)
    {

        playerController.SetControl(false);

        MatchTargetParams matchParams = null;

        yield return playerController.DoAction(animName, matchParams, Quaternion.identity);
        playerController.SetControl(true);
        playerController.IsHanging = false;
    }
    

    /// <summary>
    /// Çýkýntýya zýpla
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="ledge"></param>
    /// <param name="matchStartTime"></param>
    /// <param name="matchTargetTime"></param>
    /// <param name="hand"></param>
    /// <param name="handOffset"></param>
    /// <returns></returns>
    IEnumerator JumpToLedge(string anim, Transform ledge, float matchStartTime, float matchTargetTime, AvatarTarget hand=AvatarTarget.RightHand, Vector3? handOffset=null)
    {
        var matchParams = new MatchTargetParams()
        {
            // çýkýntý için el pozisyonu ayarlanýr
            pos = GetHandPos(ledge, hand, handOffset),
            bodyPart = hand,
            startTime = matchStartTime,
            targetTime = matchTargetTime,
            posWeight = Vector3.one
        };

        //Karakterin bakacaðý yön hesaplanýr (bakýlacak yön çýkýntýya doðrudur)
        var targetRot = Quaternion.LookRotation(-ledge.forward);

        yield return playerController.DoAction(anim, matchParams, targetRot, true);

        playerController.IsHanging = true;
    }

    /// <summary>
    /// Belirtilen çýkýntý için karakterin el pozisyonunu hesaplar
    /// </summary>
    /// <param name="ledge"></param>
    /// <param name="hand"></param>
    /// <param name="handOffset"></param>
    /// <returns></returns>
    Vector3 GetHandPos(Transform ledge, AvatarTarget hand, Vector3? handOffset)
    {
        var offVal = (handOffset != null) ? handOffset.Value : new Vector3(0.25f, 0.1f, 0.1f);

        var hDir = (hand == AvatarTarget.RightHand) ? ledge.right : -ledge.right;
        return ledge.position + ledge.forward * offVal.z + Vector3.up * offVal.y - hDir * offVal.x;
    }
}
