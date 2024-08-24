using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpawnPosition : MonoBehaviour
{
    public GameObject Head;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject LeftLeg;
    public GameObject RightLeg;
    public GameObject MidBody;
    public GameObject LowerBody;
    public GameObject UpperBody;


    public Dictionary<string, GameObject> CharacterBodyPosition;

    private void Start()
    {
        CharacterBodyPosition= new Dictionary<string, GameObject>();

        AddEntry(StringData.head, Head);
        AddEntry(StringData.leftHand, LeftHand);
        AddEntry(StringData.rightHand, RightHand);
        AddEntry(StringData.leftLeg, LeftLeg);
        AddEntry(StringData.rightLeg, RightLeg);
        AddEntry(StringData.midBody, MidBody);
        AddEntry(StringData.lowerBody, LowerBody);
        AddEntry(StringData.upperBody, UpperBody);


        void AddEntry(string key, GameObject value)
        {
            if (!CharacterBodyPosition.ContainsKey(key))
            {
                CharacterBodyPosition.Add(key, value);
            }
            else
            {
                Debug.LogWarning("Key already exists in the dictionary.");
            }
        }
    }
}
