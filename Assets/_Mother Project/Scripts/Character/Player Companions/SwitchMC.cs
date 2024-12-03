using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SwitchMC : MonoBehaviour
{
    public static SwitchMC Instance;

    public static event Action<GameObject> OnCharacterRemove;
    public static event Action OnPrevScene;

    public List<GameObject> characters = new List<GameObject>();
    int currentMainPlayerIndex = -1;

  /*  private void OnEnable()
    {
        HealthManager.OnGridDisable += Reset;
    }
    private void OnDisable()
    {
        HealthManager.OnGridDisable -= Reset;
    }*/
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        // SetMainPlayer(0);
        CharacterSwitch();
    }
    private void Update()
    {
        SwitchPlayer();
    }
    public void SwitchPlayer()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
          
            SwitchToNextCharacter();
           

        }

    }
    public void CharacterSwitch()
    {
        foreach (GameObject character in characters)
        {
            StartCoroutine(ResetCharacter(character));
            if (character.GetComponent<TemporaryStats>().isMainCharacter)
            {
                character.GetComponent<ThirdPersonController>().enabled = true;
                character.GetComponent<PlayerInput>().enabled = true;
                character.GetComponent<NavMeshAgent>().enabled = false;
                character.GetComponent<PlayerCompanions>().enabled = false;
                character.GetComponent<IsoMetricToTPS>().enabled = true;
            }
            else
            {
                character.GetComponent<ThirdPersonController>().enabled = false;
                character.GetComponent<PlayerInput>().enabled = false;
                character.GetComponent<NavMeshAgent>().enabled = true;
                character.GetComponent<PlayerCompanions>().enabled = true;
                character.GetComponent<IsoMetricToTPS>().enabled = false;
            }
        }
    }

    IEnumerator ResetCharacter(GameObject character)
    {
        // Deactivate the GameObject
        character.SetActive(false);

        // Wait for a short delay to ensure full reset
        yield return new WaitForSeconds(0.1f);

        // Reactivate the GameObject
        character.SetActive(true);
    }
    void SwitchToNextCharacter()
    {
        if (!GridSystem.instance.IsGridOn)
        {
            if (currentMainPlayerIndex != -1)
            {
                characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = false;
            }

            // StartCoroutine(ResetCharacter(currentMainPlayerIndex));
            currentMainPlayerIndex = (currentMainPlayerIndex + 1) % characters.Count;

            characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = true;

            SetMainPlayer(currentMainPlayerIndex);

            Debug.Log($"Character {currentMainPlayerIndex} is now the main player.");
        }
    
    }

    void SetMainPlayer(int index)
    {
        foreach (GameObject character in characters)
        {
            character.GetComponent<TemporaryStats>().isMainCharacter = false;
        }
        currentMainPlayerIndex = index;
        characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = true;
        CharacterSwitch(); // Apply the switch
        if(characters.Count < 2) //to the scene of other character
        {
            OnPrevScene?.Invoke();
           // LoadSceneManager.instance.LoadScene(LoadSceneManager.instance.prevScene);
        }
    }

    public void RemoveUnlinkedCharacter()
    {
        foreach (GameObject character in characters)
        {
            if (!character.GetComponent<TemporaryStats>().isLinkOn)
            {
                Debug.Log("nolinkkkkkkk "+character);
                if (!character.GetComponent<TemporaryStats>().isMainCharacter)
                {
                    Debug.Log("yessssss removeeeeeee "+character);
                    characters.Remove(character);
                    character.SetActive(false);
                    SendGameObject(character); //remove from grid
                    Debug.Log(character + " deactivate");
                    break;
                }
            }
        }
    }

    public void SendGameObject(GameObject unlinkedcharacter)
    {
        if (OnCharacterRemove != null)
        {
            OnCharacterRemove?.Invoke(unlinkedcharacter); // Send the current GameObject
        }
    }
}
