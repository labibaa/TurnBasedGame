using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    [SerializeField]
    GameObject ghostPrefab;
    public GameObject SpawnedGhost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGhost(Transform startPoint,Transform endPoint)
    {
        //SpawnedGhost = Instantiate(ghostPrefab,startPoint,endPoint);
        SpawnedGhost = Instantiate(ghostPrefab, startPoint.position, Quaternion.identity);
        SpawnedGhost.transform.SetParent(this.transform);
        //SpawnedGhost.tag = "Ghost";
        // Calculate the direction from startPoint to endPoint
        Vector3 direction = endPoint.position - startPoint.position;
        direction.y = 0f;  // Optional: Ensure the ghost faces horizontally

        // Rotate the ghost to face the endPoint
        SpawnedGhost.transform.rotation = Quaternion.LookRotation(direction);
       // SpawnedGhost.GetComponent<LerpAndLoop>().GhostModeOn(startPoint,endPoint);
    }

  

}
