using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Ortam Taray�c�
/// </summary>
public class EnvironmentScanner : MonoBehaviour
{
    /// <summary>
    /// �leri y�nl� ���n ofseti
    /// </summary>
    [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 2.5f, 0);
    /// <summary>
    /// �leri ���n uzunlu�u
    /// </summary>
    [SerializeField] float forwardRayLength = 0.8f;
    /// <summary>
    /// Yukar� y�nl� ���n uzunlu�u
    /// </summary>
    [SerializeField] float heightRayLength = 5;
    /// <summary>
    /// ��k�nt� I��n Uzunlu�u
    /// </summary>
    [SerializeField] float ledgeRayLength = 10;

    /// <summary>
    /// ��k�nt�ya t�rmanma mesafesi
    /// </summary>
    [SerializeField] float climbLedgeRayLength = 1.5f;
    /// <summary>
    /// engel layeri
    /// </summary>
    [SerializeField] LayerMask obstacleLayer;

    [SerializeField] LayerMask climbLedgeLayer;
    [SerializeField] float ledgeHeightThreshold = 0.75f;

    /// <summary>
    /// Engel Kontrol�: Karakterin �n�nde engel varsa da yoksa da ObstacleHitData d�nd�r�r
    /// </summary>
    /// <returns></returns>
    public ObstacleHitData ObstacleCheck()
    {
        var hitData = new ObstacleHitData();

        var forwardOrigin = transform.position + forwardRayOffset;
        hitData.forwardHitFound = Physics.Raycast(forwardOrigin, transform.forward, out hitData.forwardHit, forwardRayLength, obstacleLayer);

        // Debug.DrawRay(forwardOrigin, transform.forward * forwardRayLength, (hitData.forwardHitFound) ? Color.red : Color.white);

        // e�er �n�mde engel varsa
        if (hitData.forwardHitFound)
        {
            var heightOrigin = hitData.forwardHit.point + Vector3.up * heightRayLength;
            hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightHit, heightRayLength, obstacleLayer);

            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.heightHitFound) ? Color.red : Color.white);
        }
        
        return hitData;
    }

    /// <summary>
    /// ��k�nt�ya t�rmanma kontrol�: T�rmanma ger�ekle�ebilecekse true d�nd�r�r ve hit ile ilgili bilgiler ledgeHit de�i�kenine kaydedilir
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="ledgeHit"></param>
    /// <returns></returns>
    public bool ClimbLedgeCheck(Vector3 dir, out RaycastHit ledgeHit)
    {
        ledgeHit = new RaycastHit();

        // t�rmanmak i�in herhangi bir y�n se�ilmediyse false d�nd�r
        if (dir == Vector3.zero)
            return false;

        var origin = transform.position + Vector3.up * 1.5f;
        var offset = new Vector3(0, 0.18f, 0);

        // karakterin g�vdesinden yakla��k olarak ba��na kadar kademeli olarak belirtilen y�ne ���nlar g�nderir
        for (int i = 0; i < 10; i++)
        {
            Debug.DrawRay(origin + offset * i, dir);
            // Belirtilen y�ne bir ���n g�nderilir, e�er ki orada bir ��k�nt� varsa true d�ner
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
    /// �leri y�nde bir �arpma oldu mu?
    /// </summary>
    public bool forwardHitFound;
    /// <summary>
    /// Yukar� y�nde bir �arpma oldu mu?
    /// </summary>
    public bool heightHitFound;
    /// <summary>
    /// �leri y�ndeki �arpma verileri
    /// </summary>
    public RaycastHit forwardHit;
    /// <summary>
    /// Yukar� y�ndeki �arpma verileri
    /// </summary>
    public RaycastHit heightHit;
}

public struct LedgeData
{
    public float height;
    public float angle;
    public RaycastHit surfaceHit;
}
