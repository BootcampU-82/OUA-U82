using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Ortam Tarayýcý
/// </summary>
public class EnvironmentScanner : MonoBehaviour
{
    /// <summary>
    /// Ýleri yönlü ýþýn ofseti
    /// </summary>
    [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 2.5f, 0);
    /// <summary>
    /// Ýleri ýþýn uzunluðu
    /// </summary>
    [SerializeField] float forwardRayLength = 0.8f;
    /// <summary>
    /// Yukarý yönlü ýþýn uzunluðu
    /// </summary>
    [SerializeField] float heightRayLength = 5;
    /// <summary>
    /// çýkýntý Iþýn Uzunluðu
    /// </summary>
    [SerializeField] float ledgeRayLength = 10;

    /// <summary>
    /// Çýkýntýya týrmanma mesafesi
    /// </summary>
    [SerializeField] float climbLedgeRayLength = 1.5f;
    /// <summary>
    /// engel layeri
    /// </summary>
    [SerializeField] LayerMask obstacleLayer;

    [SerializeField] LayerMask climbLedgeLayer;
    [SerializeField] float ledgeHeightThreshold = 0.75f;

    /// <summary>
    /// Engel Kontrolü: Karakterin önünde engel varsa da yoksa da ObstacleHitData döndürür
    /// </summary>
    /// <returns></returns>
    public ObstacleHitData ObstacleCheck()
    {
        var hitData = new ObstacleHitData();

        var forwardOrigin = transform.position + forwardRayOffset;
        hitData.forwardHitFound = Physics.Raycast(forwardOrigin, transform.forward, out hitData.forwardHit, forwardRayLength, obstacleLayer);

        // Debug.DrawRay(forwardOrigin, transform.forward * forwardRayLength, (hitData.forwardHitFound) ? Color.red : Color.white);

        // eðer önümde engel varsa
        if (hitData.forwardHitFound)
        {
            var heightOrigin = hitData.forwardHit.point + Vector3.up * heightRayLength;
            hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightHit, heightRayLength, obstacleLayer);

            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.heightHitFound) ? Color.red : Color.white);
        }
        
        return hitData;
    }

    /// <summary>
    /// Çýkýntýya týrmanma kontrolü: Týrmanma gerçekleþebilecekse true döndürür ve hit ile ilgili bilgiler ledgeHit deðiþkenine kaydedilir
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="ledgeHit"></param>
    /// <returns></returns>
    public bool ClimbLedgeCheck(Vector3 dir, out RaycastHit ledgeHit)
    {
        ledgeHit = new RaycastHit();

        // týrmanmak için herhangi bir yön seçilmediyse false döndür
        if (dir == Vector3.zero)
            return false;

        var origin = transform.position + Vector3.up * 1.5f;
        var offset = new Vector3(0, 0.18f, 0);

        // karakterin gövdesinden yaklaþýk olarak baþýna kadar kademeli olarak belirtilen yöne ýþýnlar gönderir
        for (int i = 0; i < 10; i++)
        {
            Debug.DrawRay(origin + offset * i, dir);
            // Belirtilen yöne bir ýþýn gönderilir, eðer ki orada bir çýkýntý varsa true döner
            if (Physics.Raycast(origin + offset * i, dir, out RaycastHit hit, climbLedgeRayLength, climbLedgeLayer))
            {
                ledgeHit = hit;
                return true;
            }
        }

        return false;
    }

    public bool ObstacleLedgeCheck(Vector3 moveDir, out LedgeData ledgeData)
    {
        ledgeData = new LedgeData();

        if (moveDir == Vector3.zero)
            return false;

        float originOffset = 0.5f;
        var origin = transform.position + moveDir * originOffset + Vector3.up;

        if (PhysicsUtil.ThreeRaycasts(origin, Vector3.down, 0.25f, transform, 
            out List<RaycastHit> hits, ledgeRayLength, obstacleLayer, true))
        {
            var validHits = hits.Where(h => transform.position.y - h.point.y > ledgeHeightThreshold).ToList();

            if (validHits.Count > 0)
            {
                var surfaceRayOrigin = validHits[0].point;
                surfaceRayOrigin.y = transform.position.y - 0.1f;

                if (Physics.Raycast(surfaceRayOrigin, transform.position - surfaceRayOrigin, out RaycastHit surfaceHit, 2, obstacleLayer))
                {
                    Debug.DrawLine(surfaceRayOrigin, transform.position, Color.cyan);

                    float height = transform.position.y - validHits[0].point.y;

                    ledgeData.angle = Vector3.Angle(transform.forward, surfaceHit.normal);
                    ledgeData.height = height;
                    ledgeData.surfaceHit = surfaceHit;

                    return true;
                }
            }
        }

        return false;
    }
}

/// <summary>
/// Engel RaycasHit Verileri
/// </summary>
public struct ObstacleHitData
{
    /// <summary>
    /// Ýleri yönde bir çarpma oldu mu?
    /// </summary>
    public bool forwardHitFound;
    /// <summary>
    /// Yukarý yönde bir çarpma oldu mu?
    /// </summary>
    public bool heightHitFound;
    /// <summary>
    /// Ýleri yöndeki çarpma verileri
    /// </summary>
    public RaycastHit forwardHit;
    /// <summary>
    /// Yukarý yöndeki çarpma verileri
    /// </summary>
    public RaycastHit heightHit;
}

public struct LedgeData
{
    public float height;
    public float angle;
    public RaycastHit surfaceHit;
}
