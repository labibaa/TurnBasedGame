using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseClasses : MonoBehaviour
{
    public string characterName;
    public Sprite avatarHead;
    public string characterClass;
    [SerializeField] private string description;
    [SerializeField] private int level;
    [SerializeField] private float strength;
    [SerializeField] private float dexterity;
    [SerializeField] private float intelligence;
    [SerializeField] private float arcana;
    [SerializeField] private float skill;
    [SerializeField] private float endurance;
    [SerializeField] private float mind;
    [SerializeField] private int healthPoints;
    [SerializeField] private int resolvePoints;
    [SerializeField] float damageMultiplier;
    [SerializeField]
    protected List<ImprovedActionStat> characterAvailableActions = new List<ImprovedActionStat>();
    [SerializeField]
    protected UltimateActionsFactory playerUltimateFactory;
    protected IUltimate playerUltimate;
    [SerializeField]
    protected ActionStat warpSurge;
    [SerializeField]
    protected ActionStat groundBlast; 
    [SerializeField]
    protected ActionStat dash;


    //  [SerializeField] private int baseDamage;


    public string CharacterName { get => characterName; set => characterName = value; }
    public string Description { get => description; set => description = value; }

    public int Level { get => level; set => level = value; }
    public float Strength { get => strength; set => strength = value; }
    public float Dexterity { get => dexterity; set => dexterity = value; }
    public float Intelligence { get => intelligence; set => intelligence = value; }
    public float Arcana { get => arcana; set => arcana = value; }
    public float Skill { get => skill; set => skill = value; }
    public float Endurance { get => endurance; set => endurance = value; }
    public float Mind { get => mind; set => mind = value; }
    public int HealthPoints { get => healthPoints; set => healthPoints = value; }
    public int ResolvePoints { get => resolvePoints; set => resolvePoints = value; }
    // public int BaseDamage { get => baseDamage; set => baseDamage = value; }
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }

    //public float BaseDamage;


    // Add any abstract methods or other members as needed.
    protected virtual void Start()
    {
        if (playerUltimateFactory)
        {
            playerUltimate = playerUltimateFactory.CreateUltimate();
        }
    }
    public abstract void LevelUp();

    protected virtual void AddActionToAbility(ImprovedActionStat improvedAction) { 

        characterAvailableActions.Add(improvedAction);
    }
    protected virtual void RemoveActionToAbility(ImprovedActionStat improvedAction)
    {

        characterAvailableActions.Remove(improvedAction);
    }

    protected virtual bool IsActionUnlocked(ImprovedActionStat improvedAction)
    {

        return characterAvailableActions.Contains(improvedAction);
    }

    public List<ImprovedActionStat> GetAvailableActions()
    {
        return characterAvailableActions;
    }
    public ActionStat GetWarpAction()
    {
        return warpSurge;
    }
    public ActionStat GetDashAction()
    {
        return dash;
    }
    public ActionStat GetGroundBlastAction()
    {
        return groundBlast;
    }
    public IUltimate GetPlayerUltimate()
    {
        return playerUltimate;
    }
    public UltimateActionsFactory GetUltimateScripitable()
    {
        return playerUltimateFactory;
    }
}