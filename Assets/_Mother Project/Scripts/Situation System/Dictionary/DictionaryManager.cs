using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DictionaryManager : MonoBehaviour
{

    public static DictionaryManager instance;

    public delegate void ActionDelegate();
    public ActionDelegate action;
    private void Start()
    {

        //need to add all playable actions in the dictionary to excute them when called, actions can't have same names

        ActionMapper = new Dictionary<string, ActionDelegate>() {

        //{"GreaterStrike",ActionArchive.instance.GreaterStrike },
        //{"Devour",ActionArchive.instance.Devour},
        //{ "Threaten",ActionArchive.instance.Threaten},
        //{"CaptivatingPerformance",ActionArchive.instance.CaptivatingPerformance },
        //{"WitchesBolt", ActionArchive.instance.WitchesBolt },
        {"Block", ActionArchive.instance.Block },
        //{"Dodge", ActionArchive.instance.Dodge },
        //{"KeenSenses", ActionArchive.instance.KeenSenses },
        {"Counter", ActionArchive.instance.Counter },
        {"Heal",ActionArchive.instance.Heal},
        //{"ThirdRatePerformance", ActionArchive.instance.ThirdRatePerformance },
        //{"FearTacticts", ActionArchive.instance.FearTacticts },
        //{"Seduce", ActionArchive.instance.Seduce },

        {"MirrorMayhem",ActionArchive.instance.MirrorMayhem },
        {"AstralAnnihilation",ActionArchive.instance.AstralAnnihilation },
        {"PhantomFury",ActionArchive.instance.PhantomFury },
        {"RavenousRoast",ActionArchive.instance.RavenousRoast },
        {"MeleeAttack",ActionArchive.instance.MeleeAttack },
        {"LunarLullaby",ActionArchive.instance.LunarLullaby},
        {"HexedHavoc",ActionArchive.instance.HexedHavoc },
        {"CosmicCatastrophe",ActionArchive.instance.CosmicCatastrophe },
        {"CrystalCascade",ActionArchive.instance.CrystalCascade },
        {"VenomCloud",ActionArchive.instance.VenomCloud },
        {"PushBack",ActionArchive.instance.PushBack },
        {"DaggerThrow",ActionArchive.instance.DaggerThrow },
        {"SwordSlash",ActionArchive.instance.SwordSlash },
        {"Punch",ActionArchive.instance.Punch },
        {"Stab",ActionArchive.instance.Stab },
        {"SmokeCloud",ActionArchive.instance.SmokeCloud },
        {"BoneShield",ActionArchive.instance.BoneShield },
        {"TwoHandedArise",ActionArchive.instance.TwoHandedArise },
        {"SkeletonGrabRoud",ActionArchive.instance.SkeletonGrabRoud },
        {"SoulSteal",ActionArchive.instance.SoulSteal },
        {"SpinningAttack",ActionArchive.instance.SpinningAttack },
        {"SideAttack",ActionArchive.instance.SideAttack },
        {"HammerGroundAttack",ActionArchive.instance.HammerGroundAttack },
        {"TwoHitCombo",ActionArchive.instance.TwoHitCombo },
        {"BossSpinningAttack",ActionArchive.instance.BossSpinningAttack },
        {"FrontSlash",ActionArchive.instance.FrontSlash },
        {"ThreeHitComboOverhead",ActionArchive.instance.ThreeHitComboOverhead },
        {"ThreeHitComboSpinning",ActionArchive.instance.ThreeHitComboSpinning },
        {"AxeAttackOverhead",ActionArchive.instance.AxeAttackOverhead },
        {"DeathWheel",ActionArchive.instance.DeathWheel },
        {"FourStabCombo",ActionArchive.instance.FourStabCombo },
        {"SingleAttack",ActionArchive.instance.SingleAttack },
        {"SpearAttackPlace4High",ActionArchive.instance.SpearAttackPlace4High }


    };
    }

    Dictionary<OffenseModifier, Func<CharacterBaseClasses, float>> AttackOffence = new Dictionary<OffenseModifier, Func<CharacterBaseClasses, float>>()
        {

            { OffenseModifier.Strength,x=>x.Strength},
            { OffenseModifier.Dexterity,x => x.Dexterity},
            { OffenseModifier.StrOrArc,x => Mathf.Max(x.Strength,x.Arcana)},
            { OffenseModifier.DexOrStr,x => Mathf.Max(x.Dexterity,x.Strength)},
            { OffenseModifier.Charisma,x=>x.Intelligence}

        };


    Dictionary<DefenseModifier, Func<CharacterBaseClasses, float>> TargetDefence = new Dictionary<DefenseModifier, Func<CharacterBaseClasses, float>>()
        {
            { DefenseModifier.Endurance,x=>x.Endurance},
            {DefenseModifier.Mind,x=>x.Mind}
           // {DefenseModifier.None,  }
        };


    Dictionary<Direction, Direction> MoveDirection = new Dictionary<Direction, Direction>() {

        {Direction.right,Direction.left },
        {Direction.left,Direction.right },
        {Direction.up,Direction.down },
        {Direction.down,Direction.up },
        {Direction.none,Direction.none }
    
    
    };

    private Dictionary<string, ActionDelegate> ActionMapper;

    





    private void Awake()
    {
        instance = this;

     

    }
    
   
  

    public float GiveAttackOffenceModifier(OffenseModifier offModifier, CharacterBaseClasses player)
    {

        if (AttackOffence.TryGetValue(offModifier, out var attributeFunc))
        {
           
            return attributeFunc(player);
        }

        throw new ArgumentOutOfRangeException(nameof(offModifier), offModifier, null);
    }

    public float GiveTargetDefenceModifier(DefenseModifier defenseModifier, CharacterBaseClasses target)
    {


        if (TargetDefence.TryGetValue(defenseModifier, out var attributeFunc))
        {
            return attributeFunc(target);
        }

        throw new ArgumentOutOfRangeException(nameof(defenseModifier), defenseModifier, null);
    }

    public Direction GiveMoveDirection(Direction moveDirection)
    {
        return MoveDirection[moveDirection];
    }
     public void GiveAction(string actionName)
    {
        ActionDelegate actionToExecute =  ActionMapper[actionName];//working
        actionToExecute.Invoke();
        return;

        //if(actionName =="GreaterStrike"){
        //    ActionArchive.instance.GreaterStrike(); 
        //}
        //else if (actionName == "Devour")
        //{
        //    ActionArchive.instance.Devour();
        //}
        //else if (actionName == "Threaten")
        //{
        //    ActionArchive.instance.Threaten();
        //}
        //else if (actionName == "Devour")
        //{
        //    ActionArchive.instance.Devour();
        //}
        //else if (actionName == "Devour")
        //{
        //    ActionArchive.instance.Devour();
        //}
        //else if (actionName == "CaptivatingPerformance")
        //{
        //    ActionArchive.instance.CaptivatingPerformance();
        //}
        //else if (actionName == "WitchesBolt")
        //{
        //    ActionArchive.instance.WitchesBolt();
        //}
        //else if (actionName == "Block")
        //{
        //    ActionArchive.instance.Block();
        //}
        //else if (actionName == "Dodge")
        //{
        //    ActionArchive.instance.Dodge();
        //}
        //else if (actionName == "KeenSenses")
        //{
        //    ActionArchive.instance.KeenSenses();
        //}
        //else if (actionName == "Counter")
        //{
        //    ActionArchive.instance.Counter();
        //}
        //else if (actionName == "ThirdRatePerformance")
        //{
        //    ActionArchive.instance.ThirdRatePerformance();
        //}
        //else if (actionName == "FearTacticts")
        //{
        //    ActionArchive.instance.FearTacticts();
        //}
        //else if (actionName == "KeenSenses")
        //{
        //    ActionArchive.instance.KeenSenses();
        //}
        //else if (actionName == "Seduce")
        //{
        //    ActionArchive.instance.Seduce();
        //}
        //else if (actionName == "MeleeAttack")
        //{
        //    ActionArchive.instance.MeleeAttack();
        //}
        //else if (actionName == "RangedAttack")
        //{
        //    ActionArchive.instance.RangedAttack();
        //}
        //else if (actionName == "MirrorMayhem")
        //{
        //    ActionArchive.instance.MirrorMayhem();
        //}
        //else if (actionName == "CrystalCascade")
        //{
        //    ActionArchive.instance.CrystalCascade();
        //}
        //else if (actionName == "LunarLullaby")
        //{
        //    ActionArchive.instance.LunarLullaby();
        //}
        //else if (actionName == "HexedHavoc")
        //{
        //    ActionArchive.instance.HexedHavoc();
        //}
        //else if (actionName == "CosmicCatastrophe")
        //{
        //    ActionArchive.instance.CosmicCatastrophe();
        //}
        //else if (actionName == "RavenousRoast")
        //{
        //    ActionArchive.instance.RavenousRoast();
        //}
        //else if (actionName == "PhantomFury")
        //{
        //    ActionArchive.instance.PhantomFury();
        //}
        //else if (actionName == "AstralAnnihilation")
        //{
        //    ActionArchive.instance.AstralAnnihilation();
        //}
    }

    public ActionDelegate GiveRandomAction()
    {
        int randomActionIndex = UnityEngine.Random.Range(0, ActionMapper.Count);
        string randomActionKey = ActionMapper.Keys.ElementAt(randomActionIndex);

        return ActionMapper[randomActionKey];
    }

}

  


