using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    [SerializeField] GameObject dropdown_UI;
    [SerializeField] Button saveButton;

    String fileName;
    [SerializeField] List<GameObject> character = new List<GameObject>();

    public List<SaveTurnInformation> SavedData = new List<SaveTurnInformation>();

    public List<PlayerDataSave> SaveCharacterStats = new List<PlayerDataSave>();
    public List<PlayerDataSave> ReadCharacterStats = new List<PlayerDataSave>();

    private void Start()
    {
       // fileName = fileName + ".json";
        
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
            characterbutton.onClick.AddListener(delegate {AddCharacterData(item); });
            //characterbutton.onClick.AddListener(SaveTemporaryStatToJson);
           // characterbutton.onClick.AddListener(() => SaveDifferentCharacterData(item));
            characterbutton.onClick.AddListener(() => PrintCharacterDataFromJson(item));
            saveButton.gameObject.SetActive(true);
            saveButton.onClick.AddListener(delegate { LoadJsonToTemporaryStat(item); });
        }
    }

  
    public void AddCharacterData(GameObject character)
    {
        SaveCharacterStats.Clear();
        PlayerDataSave playerdtate = new PlayerDataSave(
              character.GetComponent<CharacterBaseClasses>().CharacterName,
              character.GetComponent<TemporaryStats>().CurrentHealth,
              character.GetComponent<TemporaryStats>().PlayerHealth,
              character.GetComponent<TemporaryStats>().CurrentAP,
              character.GetComponent<TemporaryStats>().PlayerAP,
              character.GetComponent<TemporaryStats>().CurrentDex,
              character.GetComponent<TemporaryStats>().CharacterTeam);
        SaveCharacterStats.Add(playerdtate);
        fileName = character.GetComponent<CharacterBaseClasses>().CharacterName + ".json";
        SaveTemporaryStatToJson();

    }
    public void SaveTemporaryStatToJson() //call function to save data
    {
        FileHandler.SaveToJsonData<PlayerDataSave>(SaveCharacterStats, fileName);

    }

    public void PrintCharacterDataFromJson(GameObject character)
    {
        fileName = character.GetComponent<CharacterBaseClasses>().CharacterName + ".json";
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

            CreateInputField(statInputField_txt.gameObject, "Player Name", item.Name, (value) => item.Name = value);
            CreateInputField(statInputField_txt.gameObject, "Player HP", item.PlayerHealth.ToString(), (value) => item.PlayerHealth = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Current HP", item.CurrentHealth.ToString(), (value) => item.CurrentHealth = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Current AP", item.CurrentAP.ToString(), (value) => item.CurrentAP = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Player AP", item.PlayerAP.ToString(), (value) => item.PlayerAP = int.Parse(value));
            CreateInputField(statInputField_txt.gameObject, "Player Dexterity", item.CurrentDex.ToString(), (value) => item.CurrentDex = int.Parse(value));
            CreateEnumDropdown(dropdown_UI.gameObject, "Team Name", item.CharacterTeam, (value) => item.CharacterTeam = value);

        }


    }

    public void LoadJsonToTemporaryStat(GameObject character)
    {
        fileName = character.GetComponent<CharacterBaseClasses>().CharacterName + ".json";
        ReadCharacterStats = FileHandler.LoadJsonData<PlayerDataSave>(fileName);
        foreach (var item in ReadCharacterStats)
        {
            character.GetComponent<CharacterBaseClasses>().name = item.Name;
            character.GetComponent<TemporaryStats>().PlayerHealth = item.PlayerHealth;
            character.GetComponent<TemporaryStats>().CurrentHealth = item.CurrentHealth;
            character.GetComponent<TemporaryStats>().PlayerAP = item.PlayerAP;
            character.GetComponent<TemporaryStats>().CurrentAP = item.CurrentAP;
            character.GetComponent<TemporaryStats>().CurrentDex = item.CurrentDex;
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

    public void CreateEnumDropdown(GameObject prefab, string label, TeamName defaultValue, Action<TeamName> onValueChanged)
    {
        GameObject dropdownGO = Instantiate(prefab, savedCharacterDataPanel);
        TMP_Dropdown dropdown = dropdownGO.GetComponent<TMP_Dropdown>();
        TextMeshProUGUI labelComponent = dropdownGO.transform.Find("Label_Text").GetComponent<TextMeshProUGUI>();
        if (labelComponent != null)
        {
            labelComponent.text = label;
        }

        // Clear existing options
        dropdown.ClearOptions();

        // Populate dropdown with enum values
        var enumNames = Enum.GetNames(typeof(TeamName));
        foreach (var name in enumNames)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(name));
        }

        // Set default value
        dropdown.value = Array.IndexOf(enumNames, defaultValue.ToString());
        dropdown.RefreshShownValue();

        // Add listener for when a value is selected
        dropdown.onValueChanged.AddListener((index) => {
            TeamName selectedEnum = (TeamName)Enum.Parse(typeof(TeamName), enumNames[index]);
            onValueChanged(selectedEnum);  // Invoke the callback with the selected enum value

            FileHandler.SaveToJsonData<PlayerDataSave>(ReadCharacterStats, fileName);
        });
    }

    public void SaveData()
    {
        onSaveData = true;
    }
}
