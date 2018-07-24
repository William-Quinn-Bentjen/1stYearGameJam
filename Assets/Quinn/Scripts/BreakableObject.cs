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
    public Collider bulletCollider;
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
                rb.isKinematic = false;
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
                rb.isKinematic = true;
                partsList.Add(rb);
            }
        }
        bulletCollider = GetComponent<Collider>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position + positionOffset, explosionRadius);
    }
}
