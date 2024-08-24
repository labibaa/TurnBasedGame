using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "ImprovedActionStats", menuName = "ScriptableObjects/ActionStats")]
public class ImprovedActionStat : ScriptableObject
{
    [System.Serializable]
    public struct RangeMapping
    {
        public float MinRangeValue;
        public float MaxRangeValue;
        public int MappedValue;
    }

    public RangeMapping[] RangeMappings;
    public int ActionRange;
    public float ActionAccuracy;
    public string ActionName;
    public string Description;
    public int PriorityValue;
    public int FirstPercentage;
    public int SecondPercentage;
    public int LastPercentage;
    public int APCost;

    public GameObject ParticleSystem;
    public GameObject HurtParticleSystem;
    public GameObject HitParticleSystem;

    public AudioClip actionSound;
    public GameObject actionIcon;
    public GameObject actionButton;
    public List<PlayerSoundBank> SoundReferences; 

    public VisualEffect PlayerActionVFX;
    public VisualEffect TargetHitVFX;
    public ParticleSystem particle;
    public string CharacterBodyLocation;
    public string TargetCharacterBodyLocation;
    public string TargetHurtAnimation;

    public ActionStance actionStance;
    public ActionType actionType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
