using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Orb : MonoBehaviour
{
    public float rotationDuration = 1f;
    public float moveDuration = 1f;
    public float moveDistance = 0.5f;

    private void OnEnable()
    {
        HealthManager.OnGridDisable += DestroyOrb; 
    }
    private void OnDisable()
    {
        HealthManager.OnGridDisable -= DestroyOrb;
    }

    void Start()
    {
        //// Rotate the coin continuously
        //transform.DORotate(new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);

        //// Move the coin up and down
        //transform.DOMoveY(transform.position.y + moveDistance, moveDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally")|| other.CompareTag("Player") )
        {
            other.GetComponent<TemporaryStats>().playerUltimateBarCount++;
            GameObject ultBar = other.GetComponent<TemporaryStats>().PlayerUltimateBar;
            
            ultBar.GetComponent<UltimateUI>().FillUltimateBar(1);
           
            Destroy(gameObject);
        }
    }

    void DestroyOrb()
    {
        Destroy(gameObject);
    }
}
