using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int   Level;
    public float Strength;
    public float Dexterity;
    public float Charisma;
    public float Arcana;
    public float Skill;
    public float Endurance;
    public float Mind;
    public float Health;
    public float Resolve;
    public float BaseDamge;

    public AttackType Type;
    public AttackType DealType;

    public Mood characterMood;




    FighterClass fc;
 



    public Dictionary<AttackType, float> AttackSelection;

    private void Update()
    {
       AttackSelection = new Dictionary<AttackType,float>() {
            { AttackType.Arcana, Arcana },
            { AttackType.Strength, Strength},
            { AttackType.Dexterity, Dexterity },
            {AttackType.Mind,Mind },
            {AttackType.Health, Health},
           {AttackType.Skill, Skill},
           {AttackType.Resolve, Resolve},
           {AttackType.BaseDamge, BaseDamge },
           {AttackType.Charisma, Charisma},
           {AttackType.Endurance, Endurance}
        };

        

    }

}
