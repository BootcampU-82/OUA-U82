using System.Collections;
using UnityEngine;

/// <summary>
/// T�rman�� Denetleyicisi
/// </summary>
public class ClimbController : MonoBehaviour
{
    /// <summary>
    /// �uanda �zerinde bulundu�um t�rman�� noktas�
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

            // Karakter bir eylemde bulunuyorsa yani animasyon oynat�l�yorsa ya da herhangi bir tu�a bas�lmad�ysa d�n
            if (playerController.InAction || inputDir == Vector2.zero && !Input.GetButton("Jump")) return;
            
            var neighbour = currentPoint.GetNeighbour(inputDir);
            // klavyeden tu�lara bas�ld�ysa
            if(inputDir != Vector2.zero)
            {
                // �st ��k�nt�daysam ve klavyeden yukar� tu�u ile z�plama tu�una bast�ysam
                if(currentPoint.transform.parent.CompareTag("UpperLedge") && inputDir == Vector2.up && Input.GetButton("Jump"))
                {
                    StartCoroutine(DoActionWithoutIK("BracedHangToCrouch"));
                }
                // tu�larla belirtilen y�nde kom�u yoksa d�n
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
    /// Kom�u ��k�nt�ya z�plar
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
    /// Kom�u noktaya hareket eder
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
    /// IK(Prosed�rel animasyon) olmadan eylem ger�ekle�tir
    /// </summary>
    /// <param name="animName">Oynat�lacak animasyonun ismi</param>
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
    /// ��k�nt�ya z�pla
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
            // ��k�nt� i�in el pozisyonu ayarlan�r
            pos = GetHandPos(ledge, hand, handOffset),
            bodyPart = hand,
            startTime = matchStartTime,
            targetTime = matchTargetTime,
            posWeight = Vector3.one
        };

        //Karakterin bakaca�� y�n hesaplan�r (bak�lacak y�n ��k�nt�ya do�rudur)
        var targetRot = Quaternion.LookRotation(-ledge.forward);

        yield return playerController.DoAction(anim, matchParams, targetRot, true);

        playerController.IsHanging = true;
    }

    /// <summary>
    /// Belirtilen ��k�nt� i�in karakterin el pozisyonunu hesaplar
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
