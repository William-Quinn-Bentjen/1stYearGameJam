using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWalls : MonoBehaviour {

    public LayerMask layerToCleanUp;
    private void OnTriggerEnter(Collider other)
    {
        CleanUp(other.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        CleanUp(other.gameObject);
    }
    private void CleanUp(GameObject obj)
    {
        if (layerToCleanUp == (layerToCleanUp | (1 << obj.layer)))
        {
            Destroy(obj);
        }
    }
}
