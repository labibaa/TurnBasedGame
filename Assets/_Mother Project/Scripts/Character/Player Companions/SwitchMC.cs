using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SwitchMC : MonoBehaviour
{
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> mainCameras = new List<GameObject>(); // Primary camera for each character
    public List<GameObject> secondaryCameras = new List<GameObject>(); // Secondary camera for each character

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
        }
    }

    public void CharacterSwitch()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            GameObject character = characters[i];
            bool isMainCharacter = character.GetComponent<TemporaryStats>().isMainCharacter;

            character.GetComponent<ThirdPersonController>().enabled = isMainCharacter;
            character.GetComponent<PlayerInput>().enabled = isMainCharacter;
            character.GetComponent<NavMeshAgent>().enabled = !isMainCharacter;
            character.GetComponent<PlayerCompanions>().enabled = !isMainCharacter;

            // Enable/disable cameras based on main character status
            mainCameras[i].SetActive(isMainCharacter);
            secondaryCameras[i].SetActive(isMainCharacter);
        }
    }

    IEnumerator ResetCharacter(int index)
    {
        // Deactivate the GameObject
        characters[index].SetActive(false);

        // Wait for a short delay to ensure full reset
        yield return new WaitForSeconds(0.1f);

        // Reactivate the GameObject
        characters[index].SetActive(true);
    }

    void SwitchToNextCharacter()
    {
        if (currentMainPlayerIndex != -1)
        {
            characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = false;
        }

        StartCoroutine(ResetCharacter(currentMainPlayerIndex));
        currentMainPlayerIndex = (currentMainPlayerIndex + 1) % characters.Count;

        characters[currentMainPlayerIndex].GetComponent<TemporaryStats>().isMainCharacter = true;

        SetMainPlayer(currentMainPlayerIndex);

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

        // Activate/deactivate cameras
        CharacterSwitch(); // Apply the switch
    }
}
