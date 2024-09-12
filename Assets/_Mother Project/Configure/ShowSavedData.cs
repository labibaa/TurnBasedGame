using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSavedData : MonoBehaviour
{
    bool onSaveData = false;

    [SerializeField] GameObject savedDataText;
    [SerializeField] Transform savedDataPanel;
    [SerializeField] Transform savedCharacterDataPanel;
    [SerializeField] Transform characterPanel;
    [SerializeField] TMP_InputField statInputField_txt;
    [SerializeField] Button charButton;
   
    [SerializeField] String fileName;
    [SerializeField] List<GameObject> character = new List<GameObject>();

    public List<SaveTurnInformation> SavedData = new List<SaveTurnInformation>();

    public List<PlayerDataSave> SaveCharacterStats = new List<PlayerDataSave>();
    public List<PlayerDataSave> ReadCharacterStats = new List<PlayerDataSave>();

    private void Start()
    {
        fileName = fileName + ".json";
        
    }

    public void CharacterList()
    {
        characterPanel.gameObject.SetActive(true);
        foreach (Transform child in characterPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in character)
        {
            Button characterbutton = Instantiate(charButton, characterPanel.transform);
            characterbutton.GetComponentInChildren<TextMeshProUGUI>().text = item.ToString();
            AddCharacterData(item);
            characterbutton.onClick.AddListener(delegate { PrintCharacterDataFromJson(item); });
            //characterbutton.onClick.AddListener(SaveTemporaryStatToJson);
        }
    }

    public void AddCharacterData(GameObject character)
    {
        PlayerDataSave playerdtate = new PlayerDataSave(
              character.GetComponent<CharacterBaseClasses>().CharacterName,
              character.GetComponent<TemporaryStats>().CurrentHealth,
              character.GetComponent<TemporaryStats>().PlayerHealth,
              character.GetComponent<TemporaryStats>().CurrentAP,
              character.GetComponent<TemporaryStats>().PlayerAP,
              character.GetComponent<TemporaryStats>().CurrentDex);
        SaveCharacterStats.Add(playerdtate);

    }
    public void SaveTemporaryStatToJson() //call function to save data
    {
        FileHandler.SaveToJsonData<PlayerDataSave>(SaveCharacterStats, fileName);

    }

    public void PrintCharacterDataFromJson(GameObject character)
    {

        ReadCharacterStats = FileHandler.LoadJsonData<PlayerDataSave>(fileName);
        foreach (Transform child in savedCharacterDataPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in ReadCharacterStats)
        {
            /*GameObject statInputTxt = Instantiate(statInputField_txt.gameObject, InputDataPanel.position, Quaternion.identity, InputDataPanel);
            TMP_InputField inputField = statInputTxt.GetComponent<TMP_InputField>();
            inputField.text = item.ToString();*/

            /* if(item.Name == character.name)
             {*/
            CreateInputField(statInputField_txt.gameObject, "Player Name", item.Name, (value) => item.Name = value);
                CreateInputField(statInputField_txt.gameObject, "Player HP", item.PlayerHealth.ToString(), (value) => item.PlayerHealth = int.Parse(value));
                CreateInputField(statInputField_txt.gameObject, "Current HP", item.CurrentHealth.ToString(), (value) => item.CurrentHealth = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Current AP", item.CurrentAP.ToString(), (value) => item.CurrentAP = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Player AP", item.PlayerAP.ToString(), (value) => item.PlayerAP = int.Parse(value));
                CreateInputField(statInputField_txt.gameObject, "Player Dexterity", item.CurrentDex.ToString(), (value) => item.CurrentDex = int.Parse(value));

         //   }


        }


    }

    public void PrintTurnDataFromJson()
    {
        //fileName = inputHandlerForSaving.GetFileName;
        SavedData = FileHandler.LoadJsonData<SaveTurnInformation>("zahanSave254.json");

        foreach (var item in SavedData)
        {
            // string jsonString = JsonUtility.ToJson(item, true);
            // GameObject textt = Instantiate(savedDataText.gameObject, savedCharacterDataPanel.position, Quaternion.identity, savedCharacterDataPanel);
            //TextMeshProUGUI textjs = textt.GetComponent<TextMeshProUGUI>();
            // textjs.text = jsonString;
            //textjs.text = $"Name: {item.PlayerName} \n PlayerHp: {item.PlayerHP}\n ActionName: {item.ActionName}";
            CreateInputField(statInputField_txt.gameObject, "Player Name", item.PlayerName, (value) => item.PlayerName = value);
            CreateInputField(statInputField_txt.gameObject, "Player HP", item.PlayerHP.ToString(), (value) => item.PlayerHP = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Player AP", item.PlayerAp.ToString(), (value) => item.PlayerAp = int.Parse(value));

            CreateInputField(statInputField_txt.gameObject, "Target Name", item.TargetName, (value) => item.TargetName = value);
            CreateInputField(statInputField_txt.gameObject, "Target HP", item.TargetHP.ToString(), (value) => item.TargetHP = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Target AP", item.TargetAp.ToString(), (value) => item.TargetAp = int.Parse(value));

            CreateInputField(statInputField_txt.gameObject, "Action Name", item.ActionName, (value) => item.ActionName = value);
            CreateInputField(statInputField_txt.gameObject, "Action AP Cost", item.ActionAPCost.ToString(), (value) => item.ActionAPCost = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "DateTime", item.DateTime, (value) => item.DateTime = value);

        }


    }

    public void EditableDataFromJson()
    {
        //fileName = inputHandlerForSaving.GetFileName;
        SavedData = FileHandler.LoadJsonData<SaveTurnInformation>("zahanSave254.json");

        foreach (var item in SavedData)
        {
            
        }


    }

    void CreateInputField(GameObject prefab, string label, string defaultValue, System.Action<string> onValueChanged)
    {
        // Instantiate the InputField prefab
        GameObject inputFieldGO = Instantiate(prefab, savedCharacterDataPanel);
        TMP_InputField inputField = inputFieldGO.GetComponent<TMP_InputField>();
        TextMeshProUGUI labelComponent = inputFieldGO.transform.Find("Label").GetComponent<TextMeshProUGUI>();
        if (labelComponent != null)
        {
            labelComponent.text = label;
        }
        // Set the default value
        inputField.text = defaultValue + "\n";

        // Add listener for when the value is changed
        inputField.onValueChanged.AddListener((value) => {
            onValueChanged(value);
            //if (onSaveData)
            //{
                FileHandler.SaveToJsonData<PlayerDataSave>(ReadCharacterStats, fileName);
              //  onSaveData = false;
            //}
           
        });
       
    }

    public void SaveData()
    {
        onSaveData = true;
    }
}
