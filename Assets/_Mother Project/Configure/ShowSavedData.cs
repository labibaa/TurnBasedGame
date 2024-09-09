using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowSavedData : MonoBehaviour
{
    [SerializeField] GameObject savedDataText;
    [SerializeField] Transform savedDataPanel;
    [SerializeField] Transform InputDataPanel;
    [SerializeField] TMP_InputField statInputField_txt;
    public List<SaveTurnInformation> SavedData = new List<SaveTurnInformation>();
    InputHandlerForSaving inputHandlerForSaving;
    [SerializeField] String fileName;
    [SerializeField] GameObject chracter;

    public List<int> SaveTemporaryStats = new List<int>();
    public List<int> ShowTemporaryStats = new List<int>();

    private void Start()
    {
        fileName = fileName + ".json";       
        SaveTemporaryStats.Add(chracter.GetComponent<TemporaryStats>().CurrentHealth);
        SaveTemporaryStats.Add(chracter.GetComponent<TemporaryStats>().CurrentAP);
        SaveTemporaryStats.Add(chracter.GetComponent<TemporaryStats>().CurrentDex);
    }
    public void SaveTemporaryStatToJson() //call function to save data
    {
        FileHandler.SaveToJsonData<int>(SaveTemporaryStats, fileName);

    }

    public void PrintCharacterDataFromJson()
    {

        ShowTemporaryStats = FileHandler.LoadJsonData<int>(fileName);
        foreach (var item in ShowTemporaryStats)
        {
            GameObject statInputTxt = Instantiate(statInputField_txt.gameObject, InputDataPanel.position, Quaternion.identity, InputDataPanel);
            TMP_InputField inputField = statInputTxt.GetComponent<TMP_InputField>();
            inputField.text = item.ToString();
        }


    }

    public void PrintTurnDataFromJson()
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
