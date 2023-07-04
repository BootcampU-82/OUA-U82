using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

/// <summary>
/// V�cudu rig ile kontrol etmekle ilgili i�lerden sorumlu
/// </summary>
public class BodyControl : MonoBehaviour
{
    public List<MultiAimConstraint> multiAimConstraints;

    /// <summary>
    /// Aim davran��� i�in Rig a��rl�klar�n� ayarlar
    /// </summary>
    public void SetMACWeightsAiming(float weight)
    {
        foreach(var mac in multiAimConstraints)
        {
            mac.weight = weight;
        }
    }
}
