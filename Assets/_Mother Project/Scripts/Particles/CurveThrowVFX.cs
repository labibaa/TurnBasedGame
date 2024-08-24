using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using System;

public class CurveThrowVFX : MonoBehaviour
{
    [SerializeField]float height;
    [SerializeField]float duration;
    private Transform target;
    private GameObject targetAnimator;
    public static event Action hitTrigger;

    bool Intarget;
    public void Update()
    {
        //Vector3 targetPosition = transform.position - target.position;
        //// float angle = AngleCurve * Mathf.Deg2Rad;
        //float angle;
        //float v0;
        //float time;
        //Height = target.position.y + targetPosition.magnitude/2f; 
        //CalculatePathWithHeight(targetPosition, Height, out v0, out angle,out time);
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    //StopAllCoroutines();
        //    //StartCoroutine(CoroutineCurve(v0, angle, time));
        //    //tr(target);

        //}
       // Debug.Log(transform.position + "===" + target.position);

        if (Vector3.Distance(target.position,transform.position) <= 0.0001f && !Intarget)
        {
            VisualEffect visualEffect = GetComponent<VisualEffect>();
            visualEffect.SendEvent("hit");
            visualEffect.SendEvent("stop");
            //target.gameObject.GetComponent<Animator>().Play("Damage1");
            //AnimationPlay("Damage1", targetAnimator
            Intarget = true;
            hitTrigger?.Invoke();
            targetAnimator.GetComponent<Animator>().Play("Damage1");
            Debug.Log("dhf");
        }

    }
    //float QuadraticEquation(float a, float b,float c,float sign)
    //{
    //    return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    //}

    //void CalculatePathWithHeight(Vector3 targetPos, float height, out float v0, out float angle, out float time)
    //{
    //    float xt = targetPos.x;
    //    float yt = targetPos.y;
    //    float g = - Physics.gravity.y;

    //    float b = Mathf.Sqrt(2 * g * height);
    //    float a = (-0.5f * g);
    //    float c = -yt;

    //    float tplus = QuadraticEquation(a, b, c, 1);
    //    float tminus = QuadraticEquation(a, b, c, -1);
    //    time = tplus > tminus ? tplus : tminus;

    //    angle = Mathf.Atan(b * time / xt);

    //    v0 = b / Mathf.Sin(angle);

    //}
    //IEnumerator CoroutineCurve(float v0, float angle, float tm)
    //{
    //    float t = 0;

    //    while(t < tm)
    //    {
    //        float x =  v0 * t * Mathf.Cos(angle);
    //        float y =  v0 * t * Mathf.Sin(angle) - (1f/2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
    //        transform.position = transform.position + new Vector3(x, y, 0);
    //        t += Time.deltaTime;

    //        yield return null;
    //    }
    //}

    public void tr()
    {
       
        Vector3 startPos = transform.position;
        Vector3 midPoint = startPos + (target.position - startPos) / 2f + Vector3.up * height;

        transform.DOPath(new Vector3[] { startPos, midPoint, target.position }, duration, PathType.CatmullRom)
            .SetEase(Ease.OutQuad); // You can adjust the ease type here
        
    }
    public void SetCurveTargetVFX(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetTargetAnimator(GameObject newtargetAnimator)
    {
        targetAnimator = newtargetAnimator;
    }
}
