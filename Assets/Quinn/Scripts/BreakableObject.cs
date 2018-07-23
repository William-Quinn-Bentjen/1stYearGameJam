using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public float explosiveForce = 1;
    public Vector3 positionOffset;
    public float explosionRadius = 1;
    public float upwardsModifier;
    public List<Rigidbody> partsList;
    bool broken = false;
    private void Start()
    {
        BreakObject();
    }
    public void BreakObject()
    {
        if (!broken)
        {
            broken = true;
            foreach (Rigidbody rb in partsList)
            {
                rb.AddExplosionForce(explosiveForce, gameObject.transform.position + positionOffset, explosionRadius, upwardsModifier);
            }
        }
    }
    private void Reset()
    {
        foreach (Transform child in gameObject.transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                partsList.Add(rb);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position + positionOffset, explosionRadius);
    }
}
