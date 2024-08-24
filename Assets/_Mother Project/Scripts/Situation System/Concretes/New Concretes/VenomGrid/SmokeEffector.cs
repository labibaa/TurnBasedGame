using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SmokeEffector : MonoBehaviour
{
    public bool HasEffect = false;
    public TemporaryStats EffectOwner;
    public Vector2 GridPosition;
    public int TurnCount;
    public ImprovedActionStat Smoke;
    public GameObject SmokeObject;
    public TemporaryStats target;

    // Start is called before the first frame update
    private void OnEnable()
    {
        HandleTurnNew.OnTurnEnd += DamageCurrentTarget;
    }
    private void OnDisable()
    {
        HandleTurnNew.OnTurnEnd -= DamageCurrentTarget;
    }


    void DamageCurrentTarget()
    {
        ExecuteDamageCurrentTarget();
    }

    async UniTask ExecuteDamageCurrentTarget()
    {
        if (TurnCount > 0)
        {

            GameObject gridObject = GridSystem.instance._gridArray[(int)GridPosition.x, (int)GridPosition.y].GetComponent<GridStat>().OccupiedGameObject;
            if (gridObject != null)
            {
                TemporaryStats targetTempStatsComponent = gridObject.GetComponent<TemporaryStats>();

                if (targetTempStatsComponent != null && targetTempStatsComponent != EffectOwner) // have to include a teamcheck if you want your ally's to not get affected
                {
                    targetTempStatsComponent.playerVisiblity = 0;
                    target = targetTempStatsComponent;
                    //target.playerVisiblity = 0;
                    //int diceValue = DiceNumberGenerator.instance.GetDiceValue(Smoke.FirstPercentage, Smoke.SecondPercentage, Smoke.LastPercentage);
                    //int damage = Mathf.RoundToInt(ActionResolver.instance.CalculateNewDamage(diceValue, Smoke) * EffectOwner.GetComponent<CharacterBaseClasses>().damageMultiplier);
                    //targetTempStatsComponent.CurrentHealth = HealthManager.instance.HealthCalculation(damage, targetTempStatsComponent.CurrentHealth);
                    //await HealthManager.instance.PlayerMortality(targetTempStatsComponent, 1);
                    CutsceneManager.instance.PlayAnimationForCharacter(targetTempStatsComponent.gameObject, Smoke.TargetHurtAnimation);
                }
            }


            TurnCount--;
        }
        else
        {
            if (HasEffect)
            {
                ResetEffectState();
            }

        }


    }

    private void ResetEffectState()
    {
        //target.playerVisiblity = 1;
        HasEffect = false;
        EffectOwner = null;
        GridPosition = Vector2.zero;
        TurnCount = 0;
        Smoke = null;
        Destroy(SmokeObject);
        foreach (PlayerTurn pturn in TurnManager.instance.players)
        {
            pturn.GetComponent<TemporaryStats>().playerVisiblity = 1;
        }


    }


}
