using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Xamin;

public class InputHandlerForSaving : MonoBehaviour
{
    [SerializeField] string userName;
    [SerializeField] string fileName; // use the name wanted to save the file and add .json extention
    [SerializeField] Button loginBtn;
    [SerializeField] TMP_InputField userNameInput;
    [SerializeField] TextMeshProUGUI savedDataText;
 
    public List<SaveTurnInformation> toSaveData = new List<SaveTurnInformation>(); //put the list that needs to be saved in json
    public List<SaveTurnInformation> SavedData = new List<SaveTurnInformation>(); 
    
    private void Start()
    {
        userName = System.Environment.UserName;
        int runCount = PlayerPrefs.GetInt("GameRunCount", 0);
        runCount++;
        PlayerPrefs.SetInt("GameRunCount", runCount);
        PlayerPrefs.Save();
        fileName= userName+ fileName + runCount.ToString() +".json";
    }
    public void SaveTurnToJson() //call function to save data
    {
        
        FileHandler.SaveToJsonData<SaveTurnInformation>(toSaveData,fileName);

    }

    public void LoadDataFromJson()
    {
        SavedData = FileHandler.LoadJsonData<SaveTurnInformation>(fileName);

        foreach (var item in SavedData)
        {
            savedDataText.text = item.ToString();
        }

       
    }

    public void LoadDataFromJson2()
    {
        SavedData = FileHandler.LoadJsonData<SaveTurnInformation>("C:/Users/BLI2/AppData/LocalLow/DefaultCompany/TurnBasedame/BLI2Save1157.json");

        foreach (var item in SavedData)
        {
            savedDataText.text = item.ToString();
        }


    }

    public void setUserName()
    {
        fileName = userNameInput.text;
        
        userNameInput.gameObject.SetActive(false);
    }
}
