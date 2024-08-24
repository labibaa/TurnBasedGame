using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public enum AttackTyperr
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
        BaseDamge

    };



public class DemoDmgCalc : MonoBehaviour
{
        [SerializeField]
        Character characterattacker;
        [SerializeField]
        Character characterDefender;


        //public static DamageCalculation instance;

        //private void Awake()
        //{
        //    if (instance == null)
        //    {
        //        instance = this;
        //    }
        //}

        public void Attack()
        {
            float damage = 0;
            damage = CalculateDamage(characterattacker, characterDefender, characterattacker.Type, characterDefender.Type);
            float roundupDamage = Mathf.Ceil(damage);
            Debug.Log(roundupDamage);
        }


        public void Deal()
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



        public float CalculateDamage(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack] * 0.04f)) / (Mathf.Pow(2, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }


        public float CalculateDealDamage(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }



        public float ResidualDamage(Character attacker, Character defender, AttackType attack, AttackType defense) //Same as Kill Damage calculation but with overall low base power moves. This is kept redundant in case we need to make changes later on.

        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float MovementCalculation(Character attacker, Character defender, AttackType attack, AttackType defense)

        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float ConditionInflictionCalculation(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float StaggerChance(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float LifeStealCalculation(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float KillingProbability(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float NegotiationCalculation(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float ParalysisCalculation(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float PoisonDamageCalculation(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
        }

        public float ContidionTimer(Character attacker, Character defender, AttackType attack, AttackType defense)
        {
            float damage = (attacker.BaseDamge * (attacker.AttackSelection[attack])) / (Mathf.Pow(3, (defender.AttackSelection[defense] / attacker.AttackSelection[attack])));


            return damage;
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





    

}
