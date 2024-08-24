using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonUltimateCommand : IUltimate
{
    
    CharacterBaseClasses playerCharacter;
    TemporaryStats playerTempStats;
    UltimateActionsFactory ultimateScriptable;
     

    public MonUltimateCommand(UltimateActionsFactory ultimateScritableObject)
    {
        ultimateScriptable = ultimateScritableObject;
    }
    public async void Execute()
    {
        
        List<CharacterBaseClasses> targets= GridMovement.instance.InAdjacentMatrix(playerTempStats.currentPlayerGridPosition,playerTempStats.CharacterTeam,ultimateScriptable.ultimateRange,Color.clear);
        GridMovement.instance.ResetHighlightedPath();
        playerCharacter.GetComponent<SpawnVFX>().SetVFXSound(ultimateScriptable.actionSound);
       
        CutsceneManager.instance.virtualCamera.LookAt = playerCharacter.gameObject.transform;
        CutsceneManager.instance.virtualCamera.Follow = playerCharacter.gameObject.transform;

        //CutsceneManager.instance.virtualCamera.Priority = 15;
        await CutsceneManager.instance.PlayAnimationForCharacter(playerCharacter.gameObject, "Ult1");
        foreach (CharacterBaseClasses target in targets)
        {
            TemporaryStats targetTempStats = target.GetComponent<TemporaryStats>();

            targetTempStats.CurrentHealth = HealthManager.instance.HealthCalculation(targetTempStats.CurrentHealth/2, targetTempStats.CurrentHealth);

           
            UI.instance.ShowFlyingText((targetTempStats.CurrentHealth / 2).ToString(), targetTempStats.FlyingTextParent, Color.red);
            //CutsceneManager.instance.PlayAnimationForCharacter(target.gameObject, "Hurt");
        }
        PlayerStatUI.instance.UpdateSummaryHUDUI();
        Debug.Log("Ultimate executed");
    }

    public void setValues(CharacterBaseClasses playerCh,TemporaryStats playerTemp)
    {
        playerCharacter= playerCh;
        playerTempStats= playerTemp;
    }
    public int GetultimateThreshold()
    {
       
        return ultimateScriptable.actionThreshold;
    }

    
}
