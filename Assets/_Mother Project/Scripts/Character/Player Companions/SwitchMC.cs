using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchMC : MonoBehaviour
{
    public List<GameObject> characters = new List<GameObject>();
    int currentMainPlayerIndex = -1;

    private void Start()
    {
        SetMainPlayer(0);
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
            CharacterSwitch(); // Apply the switch

        }

    }
    public void CharacterSwitch()
    {
        foreach (GameObject character in characters)
        {
            if (character.GetComponent<TemporaryStats>().isMainCharacter)
            {
                character.GetComponent<ThirdPersonController>().enabled = true;
                character.GetComponent<PlayerInput>().enabled = true;
                character.GetComponent<PlayerCompanions>().enabled = false;
            }
            else
            {
                character.GetComponent<ThirdPersonController>().enabled = false;
                character.GetComponent<PlayerInput>().enabled = false;
                character.GetComponent<PlayerCompanions>().enabled = true;
            }
        }
    }

    void SwitchToNextCharacter()
    {
        if (currentMainPlayerIndex != -1)
        {
            characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = false;
        }

        currentMainPlayerIndex = (currentMainPlayerIndex + 1) % characters.Count;

        characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = true;

        Debug.Log($"Character {currentMainPlayerIndex} is now the main player.");
    }

    void SetMainPlayer(int index)
    {
        foreach (GameObject character in characters)
        {
            character.GetComponent<TemporaryStats>().isMainCharacter = false;
        }
        currentMainPlayerIndex = index;
        characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = true;
    }

}
