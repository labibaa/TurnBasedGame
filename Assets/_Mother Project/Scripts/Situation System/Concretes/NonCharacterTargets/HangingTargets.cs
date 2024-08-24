using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingTargets : MonoBehaviour, INonCharacterTarget
{


    [SerializeField]
    GameObject TargetSelectionParticle;

    CharacterBaseClasses player;
    ImprovedActionStat scriptable;
    public async void CommandCreator() //
    {
        TempManager.instance.ChangeGameState(GameStates.MidTurn);
        //DictionaryManager.instance.GiveAction("DaggerThrow");

        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "Daggerthrow");
        player.GetComponent<TemporaryStats>().CurrentAP = ActionResolver.instance.APResolver(player.GetComponent<TemporaryStats>().CurrentAP, rangedScriptable.APCost);//ap cost
        ICommand fallTarget = new HangingTargetFall(this.gameObject,rangedScriptable);
        Turn turn = new Turn(player, fallTarget, 0);
        HandleTurnNew.instance.AddTurn(turn);
        GridMovement.instance.ResetHighlightedPath();
        TurnManager.instance.ResetTargetHIghlightVisual();
        TurnManager.instance.targetsInRange.Clear();
        TurnManager.instance.nonCharacterTargetsInRange.Clear();
    }

    public void CommandInfo(CharacterBaseClasses playerAttacker, ImprovedActionStat scriptableObject)
    {
        player = playerAttacker;
        scriptable = scriptableObject;
    }

    public GameObject GetSelectionParticle()
    {
        return this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringData.PlayerTag)|| other.CompareTag(StringData.Ally))
        {
            ICommand damage = new RangedAttack(player,other.GetComponent<CharacterBaseClasses>(),player.GetComponent<TemporaryStats>(),other.GetComponent<TemporaryStats>(),scriptable);
            damage.Execute();
        }

        if (other.CompareTag("Ground"))
        {
            TurnManager.instance.AvailableNonCharacterTargets.Remove(this.gameObject);
            Destroy(this.gameObject,1);
        }
    }

}
