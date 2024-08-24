using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject spawnPrefab;
    [SerializeField] GameObject Barrier;
    // Start is called before the first frame update
    void Start()
    {
        GameSceneManager.instance.AddToQueue(this.transform.parent.gameObject);
    }

    // Update is called once per frame
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringData.PlayerTag))
        {
            Destroy(this.gameObject);           
            GameSceneManager.instance.AddToQueue(Instantiate(spawnPrefab, this.transform.parent.position + new Vector3(0, 0, 100), Quaternion.identity));
            Barrier.SetActive(true);
            if (!GameSceneManager.instance.IsSceneLoaded1)
            {
                GameSceneManager.instance.LoadGame();
                GameSceneManager.instance.IsSceneLoaded1 = true;

            }
        }
    }
}
