using System.Runtime.CompilerServices;
using UnityEngine;


public enum AttackType
{
    Dexterity,
    Strength,
    Arcana,
    Level,
    
    
    Charisma,
   
    Skill,
    Endurance,
    Mind,
    Health,
    Resolve,
    BaseDamge

};



public class DamageCalculation : MonoBehaviour
{
    [SerializeField]
   Character characterattacker;
    [SerializeField]
    Character characterDefender;
    //Wasi's varaibles for fixed values
    float movementCost = 10f;
    bool conditionSuccess;
    bool critSuccess;
    bool staggerSuccess;
    bool moodChangeSuccess;
    bool isParalyzed;
    bool isCritical;
    float poisonDamage;
    float pDamage = 5f;

    //Wasi's Variables from Scriptable Objects

    float conditionChance;
    float critChance;
    float critModifier;
    float bonusDamage;
    float bonusDamageModifier;
    float movementPriority;
    float weaknessModifier;
    float staggerChance;
    float moodModifier;
    float lifeStealAmount;

    //Wasi's variables that will change overtime
    float healthDamage;
    float resolveDamage;
    bool targetisDead;


    public static DamageCalculation instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void KillDamage()
    { 
        float damage = 0;
        damage = CalculateKillDamage(characterattacker,characterDefender,characterattacker.Type,characterDefender.Type);
        float roundupDamage = Mathf.Ceil(damage);
        Debug.Log(roundupDamage);
    }


    public void DealDamage()
    {
        float emotionalDamage = 0;
        emotionalDamage = CalculateDealDamage(characterattacker, characterDefender, characterattacker.DealType, characterDefender.DealType);
        float roundupDamage = Mathf.Ceil(emotionalDamage);
        Debug.Log(roundupDamage);
    }


    //public void Attack()
    //{

    //    if(type == 0 )
    //    {
    //        float damageValue = CalculateDamage(BaseDamge,Strength,Endurance);
    //        Debug.Log(damageValue);
    //    }
    //    else if(type == 1 )
    //    {
    //        float damageValue = CalculateDamage(BaseDamge, Dexterity, Endurance);
    //        Debug.Log(damageValue);
    //    }
    //    else if( type == 2 )
    //    {

    //        float damageValue = CalculateDamage(BaseDamge, Arcana, Endurance);
    //        Debug.Log(damageValue);
    //    }





    //}

    //add functions for modifier

    public float CalculateKillDamage(Character attacker, Character defender, AttackType attack,AttackType defense) // damage calculation of actions
    {
        healthDamage = (((attacker.BaseDamge + bonusDamage) * (attacker.AttackSelection[attack] * 0.04f) ) / (Mathf.Pow(2,(defender.AttackSelection[defense] / attacker.AttackSelection[attack]))) * critModifier * weaknessModifier);


        return healthDamage;
    }

    //add functions for modifier

    public float CalculateDealDamage(Character attacker, Character defender, AttackType attack, AttackType defense)
    {
        resolveDamage = (((attacker.BaseDamge + bonusDamage) * (attacker.AttackSelection[attack] )) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack]))) * critModifier * moodModifier);


        return resolveDamage;
    }


    public float ResidualDamage(Character attacker, Character defender, AttackType attack, AttackType defense) //Same as Kill Damage calculation but with overall low base power moves. This is kept redundant in case we need to make changes later on.

    {
        float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


        return damage;
    }

    public float MovementCostReductionCalculation()
    {
        //need a senior engineer for this.
        // Each position in the grid costs 10 Priority Value,
        // that amount will reduce with dexterity by 0.10 per dexterity point rounded up.
        // The Priority Value reduction decreases by 0.01 after each point of dexterity.
        // So the reduction of priority value becomes slower gradually to balance the game.
        // 
        return movementPriority;
    }

    public float MovementCalculation()  //(Character attacker, Character defender, AttackType attack, AttackType defense) 
    {
        //calculates movement for each tile in the grid.
        if(movementPriority != 0)
        {
          movementCost = movementCost - movementPriority;
        }
        else
        {
            movementCost = 10f;
        }
        
        return movementCost;
    }

    

    public bool ConditionInflictionCalculation() //(Character attacker, Character defender, AttackType attack, AttackType defense) 
    {
        float checkCondition = Random.Range(1, 100);
        if(checkCondition <= conditionChance)
        {
            conditionSuccess = true;
        }
        else
        {
            conditionSuccess = false;
        }
        return conditionSuccess;
    }

    public bool StaggerChance() //(Character attacker, Character defender, AttackType attack, AttackType defense) 
    {
        float checkStagger = Random.Range(1, 100);
        if (checkStagger >= staggerChance)
        {
            staggerSuccess = true;
        }
        else
        {
            staggerSuccess = false;
        }
        return staggerSuccess;
    }

    public bool MoodChangeCalculation()
    {
        float checkMoodChangeSuccess = Random.Range(1, 100);
        if (checkMoodChangeSuccess <= conditionChance * moodModifier)
        {
            moodChangeSuccess = true; 
        }
        else
        {
            moodChangeSuccess = false;   
        }
        return moodChangeSuccess;
    }

    public float LifeStealCalculation() //(Character attacker, Character defender, AttackType attack, AttackType defense) 
    {
        lifeStealAmount = healthDamage / conditionChance;
        return lifeStealAmount;
    }

    public float KillingLifeSteal() //(Character attacker, Character defender, AttackType attack, AttackType defense) 
    {
      
        if (targetisDead)
        {
            lifeStealAmount = healthDamage / conditionChance;
        }

        return lifeStealAmount;
    }

    public bool NegotiationCalculation() //(Character attacker, Character defender, AttackType attack, AttackType defense)
    {
        //WILL TARGET LATER AFTER PLAYTESTING
        return false;
    }

    public bool ParalysisCalculation() //(Character attacker, Character defender, AttackType attack, AttackType defense) 
    {
        int paralysisChance = Random.Range(0, 100);
        if(paralysisChance >= 30)
        {
            isParalyzed = true;
        }
        else
        {
            isParalyzed = false;
        }
        return false;
    }

    public float PoisonDamageCalculation(Character attacker, Character target, AttackType defence) //(Character attacker, Character defender, AttackType attack, AttackType defense) 
    {
        poisonDamage = (pDamage / Mathf.Pow(2, target.AttackSelection[defence]));
        return poisonDamage;
    }

    public int ContidionTimer() //(Character attacker, Character defender, AttackType attack, AttackType defense)
    {
        //basic timer
        //everytime a round ends, -- the condition
        return 0;
    }

    public int IncreaseStat()
    {
        int a = 0;
        return a;
    }

    public float CritchanceCalculation()
    {
        float c = 0;
        return c;
    }


    void CalculateMelee()
    {

       int targetNUmber = Random.Range(0, 100);
    }


}

