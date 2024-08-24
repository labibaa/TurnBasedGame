using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour,IInteractable
{
    public GameObject BrokenPrefab;
    [SerializeField] private GameObject ClashParticlePf ;

   /* public static event Action OnDestroy;


    private void OnEnable()
    {
        OnDestroy += SpawnOnLoad;
    }
    private void OnDisable()
    {
        OnDestroy -= SpawnOnLoad;
    }*/

    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.L))
        {
            //OnDestroy?.Invoke();
            GameObject Broken =  Instantiate(BrokenPrefab, transform.position, transform.rotation,transform.parent);
          
            Destroy(gameObject);
        }*/
    }
    void SpawnOnLoad()
    {
        //OnDestroy?.Invoke();
        GameObject Broken = Instantiate(BrokenPrefab, transform.position, transform.rotation, transform.parent);

        Destroy(gameObject);
    }

    public void Interact()
    {
        SpawnOnLoad();
    }
}
