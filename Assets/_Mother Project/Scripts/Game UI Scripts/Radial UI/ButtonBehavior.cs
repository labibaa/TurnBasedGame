using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject detailsPrefab;
    private GameObject detailsInstance;
    private RectTransform canvasRect;

    ImprovedActionStat actionScriptable;
    [SerializeField]
    string actionName;
    string directory = "ActionMoves/";
    [SerializeField]
    TMP_Text actionNameText;
    [SerializeField]
    TMP_Text apCost;
    [SerializeField]
    TMP_Text range;

    private void Start()
    {
        actionScriptable = DAOScriptableObject.instance.GetImprovedActionData(directory, actionName);

        if (actionScriptable != null)
        {

            actionNameText.text = actionScriptable.ActionName;
            apCost.text = "Ap Cost : " + actionScriptable.APCost;
            range.text = "Range : " + actionScriptable.ActionRange;
        }









    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (actionName == "Move")
        {
            ShowMoveRange();
            return;

        }

        if (detailsPrefab != null && actionScriptable != null)
        {

            actionNameText.text = actionScriptable.ActionName; ;
            apCost.text = "Ap Cost : " + actionScriptable.APCost;
            range.text = "Range : " + actionScriptable.ActionRange;
            detailsPrefab.SetActive(true);
            ShowActionRange();

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (actionScriptable != null || actionName == "Move")
        {
            detailsPrefab.SetActive(false);
            HideActionRange();
        }
    }

    void ShowActionRange()
    {
        GridMovement.instance.InAdjacentMatrix(TempManager.instance.attacker.GetComponent<TemporaryStats>().currentPlayerGridPosition, TempManager.instance.attacker.GetComponent<TemporaryStats>().CharacterTeam, actionScriptable.ActionRange,Color.red);
    }

    void ShowMoveRange()
    {
        GridMovement.instance.InAdjacentMatrix(TempManager.instance.attacker.GetComponent<TemporaryStats>().currentPlayerGridPosition, TempManager.instance.attacker.GetComponent<TemporaryStats>().CharacterTeam,TempManager.instance.attacker.GetComponent<TemporaryStats>().CurrentDex,Color.red);
    }

    void HideActionRange()
    {
        GridMovement.instance.ResetHighlightedPath();
    }

}
