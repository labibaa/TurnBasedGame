using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using StarterAssets;

public class QTESys : MonoBehaviour
{
    public GameObject DisplayBox;
    public GameObject PassBox;
    public float timeLimit = 3.0f;
    public float minTimeLimit = 1.0f;
    public float timeDecreaseAmount = 0.2f;
    public int increaseFrequency = 3;
    public int maxSequenceSize = 6;
    public int winningStreakToEnd = 2;

    private List<KeyCode> QTEGen = new List<KeyCode>();
    private List<KeyCode> inputSequence = new List<KeyCode>();
    private bool waitingForInput = false;
    private int successfulAttempts = 0;
    private int currentSequenceLength = 3;
    private int currentLetterIndex = 0;
    [SerializeField] PlayerActivator PlayerActivator;

    private void Start()
    {
        PassBox.GetComponent<TextMeshProUGUI>().text = "";
        GenerateRandomSequence();
        PlayerActivator.ActivatePlayer();


    }

    private void GenerateRandomSequence()
    {
        QTEGen.Clear();
        for (int i = 0; i < currentSequenceLength; i++)
        {
            int randomIndex = Random.Range(0, 4);
            KeyCode randomKey = GetKeyCodeByIndex(randomIndex);
            QTEGen.Add(randomKey);
        }

        DisplaySequence();
    }

    private void DisplaySequence()
    {
        // Check if we have displayed all letters
        if (currentLetterIndex >= QTEGen.Count)
        {
            StartCoroutine(WaitAndHideDisplayBox());
            return;
        }

        // Get the next letter to display
        KeyCode nextKeyCode = QTEGen[currentLetterIndex];
        string generatedLetter = nextKeyCode.ToString();

        // Append the current letter to the existing text
        string currentText = DisplayBox.GetComponent<TextMeshProUGUI>().text;
        currentText += generatedLetter + " ";
        DisplayBox.GetComponent<TextMeshProUGUI>().text = currentText;

        // Show the DisplayBox with a tween from the bottom
        Vector3 originalPosition = DisplayBox.transform.localPosition;
        Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y - 100f, originalPosition.z);
        DisplayBox.transform.localPosition = targetPosition;
        DisplayBox.SetActive(true);

        DisplayBox.transform.DOLocalMoveY(originalPosition.y, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            // After displaying the letter, increment the letter index
            currentLetterIndex++;
            // Call DisplaySequence() recursively to display the next letter
            DisplaySequence();
        });
    }

    private IEnumerator WaitAndHideDisplayBox()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before hiding the DisplayBox
        DisplayBox.SetActive(false); // Hide the DisplayBox
        waitingForInput = true;
        StartCoroutine(StartWaitingForInput());
    }

    private KeyCode GetKeyCodeByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return KeyCode.H;
            case 1:
                return KeyCode.J;
            case 2:
                return KeyCode.K;
            case 3:
                return KeyCode.L;
            default:
                return KeyCode.None;
        }
    }

    private IEnumerator StartWaitingForInput()
    {
        float currentLimit = timeLimit;
        while (currentLimit > 0f)
        {
            yield return null;
            currentLimit -= Time.deltaTime;
            if (waitingForInput)
            {
                if (Input.GetKeyDown(KeyCode.H))
                {
                    inputSequence.Add(KeyCode.H);
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    inputSequence.Add(KeyCode.J);
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    inputSequence.Add(KeyCode.K);
                }
                else if (Input.GetKeyDown(KeyCode.L))
                {
                    inputSequence.Add(KeyCode.L);
                }
            }
        }

        waitingForInput = false;
        CheckInput();
    }

    private void CheckInput()
    {
        bool isPass = true;

        if (inputSequence.Count != QTEGen.Count)
            isPass = false;
        else
        {
            for (int i = 0; i < inputSequence.Count; i++)
            {
                if (inputSequence[i] != QTEGen[i])
                {
                    isPass = false;
                    break;
                }
            }
        }

        if (isPass)
        {
            successfulAttempts++;
            if (successfulAttempts >= winningStreakToEnd)
            {
                StartCoroutine(DisplayCongratulationsAndEndGame());
                //DisplayCongratulationsAndEndGame();
                return; // Exit early to prevent ResetQTE from being called
            }
            else
            {
                PassBox.GetComponent<TextMeshProUGUI>().text = "Pass";
                DisplayBox.GetComponent<TextMeshProUGUI>().text = "";
                if (successfulAttempts >= increaseFrequency && currentSequenceLength < maxSequenceSize)
                {
                    currentSequenceLength++;
                    timeLimit = Mathf.Max(timeLimit - timeDecreaseAmount, minTimeLimit);
                }
            }
        }
        else
        {
            PassBox.GetComponent<TextMeshProUGUI>().text = "Fail";
            DisplayBox.GetComponent<TextMeshProUGUI>().text = "";
            currentSequenceLength = 3;
            successfulAttempts = 0;
            timeLimit = 3.0f;
        }

        StartCoroutine(ResetQTE());
    }

    private IEnumerator DisplayCongratulationsAndEndGame()
    {
        DisplayBox.SetActive(true);
        DisplayBox.GetComponent<TextMeshProUGUI>().text = "Congrats!";
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds to display "Congratulations"
        EndGame();
    }

    private void EndGame()
    {
        // Add your code here to end the game, e.g., transition to a different scene or reset the game.
        // For example, you can reload the current scene:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
        PlayerActivator.DeActivatePlayer();
    }

    private IEnumerator ResetQTE()
    {
        yield return new WaitForSeconds(2.0f);
        PassBox.GetComponent<TextMeshProUGUI>().text = "";
        inputSequence.Clear();
        currentLetterIndex = 0; // Reset the letter index
        GenerateRandomSequence();
    }
}
