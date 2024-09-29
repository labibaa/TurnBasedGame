using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterClass : CharacterBaseClasses
{

    protected override void Start()
    {
        base.Start();
       
    }


    public override void LevelUp()
    {
        // Increase the character's level by 1.
        Level++;

        // Increase the character's attributes based on a predetermined formula.
        Strength += 2;
        Dexterity += 3;
        Intelligence += 1;
        Arcana += 1;
        Endurance += 2;
        DamageMultiplier += 0.6f;
        MaxExperiencePoint += 100;
        // Increase the character's maximum health based on a predetermined formula.
        HealthPoints += 10 + (Endurance * 2);
       

        // Add any additional logic or side effects as needed.
    }

}
