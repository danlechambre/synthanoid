using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactVFX : MonoBehaviour
{
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.main.startLifetimeMultiplier);
    }
}
