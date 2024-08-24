using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UltimateUI : MonoBehaviour
{
    public TextMeshPro ultimateBarText;
    [SerializeField]Image ultimateBar;
    public float ultimateBarProgress,maxProgress;
    float lerpSpeed; 
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        ultimateBar = GetComponent<Image>();
        ultimateBar.fillAmount= 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lerpSpeed = 3f* Time.deltaTime;
        ColorChanger();
    }
    public void UltimateBarFiller()
    {
        if (ultimateBar == null)
        {
            ultimateBar = GetComponent<Image>();
        }
        ultimateBar.fillAmount =  ultimateBarProgress / maxProgress; 
    }
    public void ResetUltimateBar()
    {
        Debug.Log("hh");
        ultimateBar.fillAmount = 0.0f;
        ultimateBarProgress = 0;
        
    }

    public void FillUltimateBar(int ultimateProgressCount)
    {
        if (ultimateBarProgress < maxProgress)
        {
            ultimateBarProgress += ultimateProgressCount;
        }
        else
        {
            ultimateBarProgress= maxProgress;
        }
        UltimateBarFiller();
    }

    void ColorChanger()
    {
        Color ultimateColor =  Color.Lerp(Color.red,Color.green, ultimateBarProgress / maxProgress);
        ultimateBar.color = ultimateColor;
    }
}
