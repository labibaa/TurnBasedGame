using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowSavedData : MonoBehaviour
{
    [SerializeField] GameObject savedDataText;
    [SerializeField] Transform savedDataPanel;
    public List<SaveTurnInformation> SavedData = new List<SaveTurnInformation>();
    InputHandlerForSaving inputHandlerForSaving;
    String fileName;
    public void LoadDataFromJson2()
    {
        //fileName = inputHandlerForSaving.GetFileName;
        SavedData = FileHandler.LoadJsonData<SaveTurnInformation>("BLI2Save1157.json");

        foreach (var item in SavedData)
        {
            string jsonString = JsonUtility.ToJson(item, true);
            GameObject textt = Instantiate(savedDataText.gameObject, savedDataPanel.position, Quaternion.identity, savedDataPanel);
            TextMeshProUGUI textjs = textt.GetComponent<TextMeshProUGUI>();
            textjs.text = jsonString;
        }


    }
}
