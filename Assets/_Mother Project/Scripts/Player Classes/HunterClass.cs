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
        Strength += 2f;
        Dexterity += 3f;
        Intelligence += 0f;
        Arcana += 1f;
        Endurance += 1f;
        DamageMultiplier += 0.6f;
        // Increase the character's maximum health based on a predetermined formula.
        //Health += 10f + (Endurance * 2f);
       

        // Add any additional logic or side effects as needed.
    }

}
