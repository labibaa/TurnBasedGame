using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float delay = 5f;  // Time in seconds before the object is destroyed

    void Start()
    {
        StartCoroutine(DestroyAfterTime(delay));
    }

    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
