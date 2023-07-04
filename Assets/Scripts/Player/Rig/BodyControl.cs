using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

/// <summary>
/// Vücudu rig ile kontrol etmekle ilgili iþlerden sorumlu
/// </summary>
public class BodyControl : MonoBehaviour
{
    public List<MultiAimConstraint> multiAimConstraints;

    /// <summary>
    /// Aim davranýþý için Rig aðýrlýklarýný ayarlar
    /// </summary>
    public void SetMACWeightsAiming(float weight)
    {
        foreach(var mac in multiAimConstraints)
        {
            mac.weight = weight;
        }
    }
}
