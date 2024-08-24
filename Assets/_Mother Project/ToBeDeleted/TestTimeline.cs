using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimeline : MonoBehaviour
{
    public List<int> testNo = new List<int>();

    public int timer;
    // Start is called before the first frame update
    void Start()
    {
        Do();
    }

    public void Do()
    {

        timer++;

        if (timer < testNo[testNo.Count-1])
        {


            Invoke("Do", 1f);

            for (int i = 0; i < testNo.Count; i++)
            {
                if (timer == testNo[i])
                {
                    StartCoroutine(Tester());
                }
            }
        }
    }




    public IEnumerator Tester()
    {
        for(int i = 0; i < testNo.Count; i++)
        {

            testNo[i]++; //zcheckListValue
            yield return new WaitForSeconds(10f);
            testNo[i] = 0;
        }
    }
}
