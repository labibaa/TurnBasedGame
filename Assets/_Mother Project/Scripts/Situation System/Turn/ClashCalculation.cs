using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClashCalculation : MonoBehaviour
{
   public CharacterBaseClasses GetPVClashLooser(CharacterBaseClasses player1, int pvOFplayer1,
      CharacterBaseClasses player2, int pvOFplayer2)
   {
      CharacterBaseClasses looserOfPVClash;
      looserOfPVClash =
         (200 - pvOFplayer1) * player1.Skill * Random.Range(0.8f, 1.2f) <
         (200 - pvOFplayer2) * player2.Skill * Random.Range(0.8f, 1.2f)
            ? player1
            : player2;
      return looserOfPVClash;
   }
}
