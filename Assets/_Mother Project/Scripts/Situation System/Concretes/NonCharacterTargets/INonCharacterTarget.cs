using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INonCharacterTarget 
{

    
    public void CommandCreator();
    public void CommandInfo(CharacterBaseClasses playerAttacker,ImprovedActionStat scriptableObject);
    public GameObject GetSelectionParticle();

}
