using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWalls : MonoBehaviour {
    public float verticleSize = 1000;
    public float radius = 40;
    public float offset = 1000;
    public float cleanUpInterval = 5;
    //public LayerMask layerToCleanUp;
    //private void OnTriggerEnter(Collider other)
    //{
    //    CleanUp(other.gameObject);
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    CleanUp(other.gameObject);
    //}
    //private void CleanUp(GameObject obj)
    //{
    //    if (layerToCleanUp == (layerToCleanUp | (1 << obj.layer)))
    //    {
    //        Destroy(obj);
    //    }
    //}
    private void Start()
    {
        StartCoroutine(DeleteOld());
    }
    IEnumerator DeleteOld()
    {
        while(true)
        {
            Vector3 bottom = gameObject.transform.position + new Vector3(0, offset, 0);

            foreach(Collider col in Physics.OverlapCapsule(bottom, bottom + new Vector3(0, verticleSize, 0), radius))
            {
                Destroy(col.gameObject);
                //if (col.GetComponent<PooledObject>() != null)
                //return to pool
            }
            yield return new WaitForSeconds(cleanUpInterval);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position + new Vector3(0, offset + (.5f * verticleSize), 0), new Vector3(radius*2,verticleSize, radius*2));
    }
}
