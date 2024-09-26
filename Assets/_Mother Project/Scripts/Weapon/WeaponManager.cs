using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    [SerializeField] protected List<ImprovedActionStat> DaggerAvailableActions = new List<ImprovedActionStat>();
    [SerializeField] protected List<ImprovedActionStat> SwordAvailableActions = new List<ImprovedActionStat>();
    [SerializeField] protected List<ImprovedActionStat> BowAndArrowAvailableActions = new List<ImprovedActionStat>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public List<ImprovedActionStat> GetDaggerAvailableActions()
    {
        return DaggerAvailableActions;
    }
    public List<ImprovedActionStat> GetSwordAvailableActions()
    {
        return SwordAvailableActions;
    }
    public List<ImprovedActionStat> GetBowAndArrowAvailableActions()
    {
        return BowAndArrowAvailableActions;
    }
}
