using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionArchive : MonoBehaviour
{



    public static ActionArchive instance;
    
    TemporaryStats playerInfo;
    TemporaryStats targetInfo;
    [SerializeField] List<GridInput> gridInput = new List<GridInput>();
    [SerializeField] PlayerStatUI playerStateUI;

    CharacterBaseClasses playerAttacker;
    CharacterBaseClasses targetDefender;
    TemporaryStats currentStatPlayer;
    TemporaryStats currentStatTarget;


    public bool isTurnAdded = false;
    //[SerializeField] ActionNotification actionNotification;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        //DictionaryManager.instance.action += GreaterStrike;
        //DictionaryManager.instance.action += CaptivatingPerformance;
    }

    public void GetPlayerStats()
    {

        // Move Later

        UI.instance.ClearTargetList();
        playerAttacker = TempManager.instance.attacker.GetComponent<CharacterBaseClasses>();
        currentStatPlayer = playerAttacker.GetComponent<TemporaryStats>();
        if (TempManager.instance.defender)
        {
            targetDefender = TempManager.instance.defender.GetComponent<CharacterBaseClasses>();
            currentStatTarget = targetDefender.GetComponent<TemporaryStats>();
        }




    }

    public async void WitchesBolt()
    {
        GetPlayerStats();
        ActionStat witchesBoltScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "WitchesBolt");
        ICommand witchesBoltAction = new WitchsBolt(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, witchesBoltScriptable);
        ActionTemplate(witchesBoltScriptable, witchesBoltAction);
    }

    public void Choke(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStat)
    {
        ActionStat chokeScriptable = Resources.Load<ActionStat>("ActionMoves/Choke"); // will have to change later, and make it a global variable. cz as for now everytime this function is being called, the variable is getting loaded .
        float damageValue = ActionResolver.instance.CalculateKillDamage(playerAttacker, targetDefender, chokeScriptable);
        currentStat.CurrentHealth = HealthManager.instance.HealthCalculation(damageValue, currentStat.CurrentHealth);
        playerInfo = playerAttacker.gameObject.GetComponent<TemporaryStats>();

        playerInfo.CurrentAP = ActionResolver.instance.APResolver(playerInfo.CurrentAP, chokeScriptable.APCost);

        Debug.Log("Damage :" + damageValue);
    }







    public async void Devour()
    {
        GetPlayerStats();
        ActionStat devourScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "Devour");
        ICommand devourAction = new Devour(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, devourScriptable);
        ActionTemplate(devourScriptable, devourAction);
    }

    public async void GreaterStrike()
    {
        GetPlayerStats();
        ActionStat greaterStrikeScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "GreaterStrike");
        ICommand greaterStrikeAction = new GreaterStrike(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, greaterStrikeScriptable);
        ActionTemplate(greaterStrikeScriptable, greaterStrikeAction);
    }

    public async void MeleeAttack()
    {
        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "MeleeAttack");
       
        bool isMoveAdded =MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable,"SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
       
    }





    public async void CosmicCatastrophe()
    {
        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "CosmicCatastrophe");

        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }

    public async void RavenousRoast()
    {
        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "RavenousRoast");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }


    public async void PhantomFury()
    {
        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "PhantomFury");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }

    public async void AstralAnnihilation()
    {
        
        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "AstralAnnihilation");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }

    public async void SwordSlash()
    {

        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "SwordSlash");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }
    public async void BoneShield()
    {

        
        GetPlayerStats();

        ImprovedActionStat boneShieldScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "BoneShield");
        ICommand BoneShieldAction = new BoneShield(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, boneShieldScriptable);
        ActionTemplate(boneShieldScriptable, BoneShieldAction);

        //GetPlayerStats();
        //ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "BoneShield");

        //ICommand rangedAction = new BoneShield(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        //ActionTemplate(rangedScriptable, rangedAction);
    }

    public async void TwoHandedArise()
    {

        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "TwoHandedArise");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }

    public async void SkeletonGrabRoud()
    {

        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "SkeletonGrabRoud");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }

    public async void SoulSteal()
    {

        //GetPlayerStats();
        //ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "SoulSteal");
        //bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        //if (isMoveAdded)
        //{
        //    ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
        //    ActionTemplate(meleeScriptable, meleeAction);
        //}
        //else
        //{
        //    ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
        //    ActionTemplate(meleeScriptable, meleeAction);
        //}


        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "SoulSteal");
        ICommand rangedAction = new RangedAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);

    }


    public async void Stab()
    {

        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "Stab");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }
    public async void Punch()
    {

        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "Punch");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new MeleeAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }

    public async void PushBack()
    {

        GetPlayerStats();
        ImprovedActionStat meleeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "PushBack");
        bool isMoveAdded = MeleeMoveTemplate(meleeScriptable);
        if (isMoveAdded)
        {
            ICommand meleeAction = new PushBack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "Melee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
        else
        {
            ICommand meleeAction = new PushBack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, meleeScriptable, "SingleMelee");
            ActionTemplate(meleeScriptable, meleeAction);
        }
    }

    public async void Heal()
    {
        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "Heal");

        ICommand rangedAction = new Heal(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);
    }


    public bool MeleeMoveTemplate(ImprovedActionStat actionScriptable)
    {
        if (ActionResolver.instance.APResolver(currentStatPlayer.CurrentAP, actionScriptable.APCost) < 0) {
            return false;
        }
        Vector2 playerPosition = GridSystem.instance.WorldToGrid(playerAttacker.GetComponent<TemporaryStats>().currentPlayerGridPosition);
        List<GameObject> path = new List<GameObject>();
        List<GameObject> pathToGo = new List<GameObject>();
        Vector2 targetPosition;
       
        if (!GridMovement.instance.InAdjacentMatrix(playerAttacker.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, targetDefender.transform.position, 1))
        {

            targetPosition = AutoGridMovement.instance.CheckClosestAdjacent(playerAttacker.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, targetDefender.transform.position);


            path = AutoGridMovement.instance.FindPath(playerPosition, targetPosition);

            pathToGo.Add(path[0]);
            pathToGo.Add(path[path.Count - 1]);
        }

        if (path.Count > 1)
        {
            playerAttacker.GetComponent<TemporaryStats>().AutoMove = true;
            playerAttacker.GetComponent<TemporaryStats>().currentPlayerGridPosition = path[path.Count - 1].transform.position;

            ICommand MovementConrete = new Move(pathToGo, playerAttacker.GetComponent<NavMeshAgent>(), true,"MeleeMove");
            //await GameManager.instance.AddCommand(MovementConrete);
            Turn turn = new Turn(playerAttacker.gameObject.GetComponent<CharacterBaseClasses>(), MovementConrete, 20);
            HandleTurnNew.instance.AddTurn(turn);

            return true;
        }
        else
        {
            return false;
        }
    }




    public async void RangedAttack()
    {
        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "RangedAttack");
        
        ICommand rangedAction = new RangedAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);
    }

    public async void VenomCloud()
    {
        GetPlayerStats();
        ImprovedActionStat venomScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "VenomCloud");

        ICommand venomAction = new VenomCloud(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, venomScriptable);
        ActionTemplate(venomScriptable, venomAction);
        GridMovement.instance.InAdjacentMatrix(currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, TeamName.NullTeam, venomScriptable.ActionRange, Color.red);
    }
    public async void SmokeCloud()
    {
        GetPlayerStats();
        ImprovedActionStat smokeScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "SmokeCloud");

        ICommand smokeAction = new SmokeCloud(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, smokeScriptable);
        ActionTemplate(smokeScriptable, smokeAction);
        GridMovement.instance.InAdjacentMatrix(currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, TeamName.NullTeam, smokeScriptable.ActionRange, Color.red);
    }


    public async void MirrorMayhem()
    {
        
        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "MirrorMayhem");
        ICommand rangedAction = new RangedAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);
        
    }
    public async void DaggerThrow()
    {

        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "Daggerthrow");
        ICommand rangedAction = new RangedAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);

    }
    public async void CrystalCascade()
    {
        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "CrystalCascade");
        ICommand rangedAction = new RangedAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);
    }

    public async void LunarLullaby()
    {
        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "LunarLullaby");
        ICommand rangedAction = new RangedAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);
    }

    public async void HexedHavoc()
    {
        GetPlayerStats();
        ImprovedActionStat rangedScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "HexedHavoc");
        ICommand rangedAction = new RangedAttack(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, rangedScriptable);
        ActionTemplate(rangedScriptable, rangedAction);
    }



    //same type of actions needs code refactoring
    public async void Threaten()
    {
        GetPlayerStats();
        ActionStat threatenScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "Threaten");
        ICommand threatenAction = new Threaten(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, threatenScriptable);
        ActionTemplate(threatenScriptable, threatenAction);
    }
    public async void Seduce()
    {
        GetPlayerStats();
        ActionStat seduceScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "Seduce");
        ICommand seduceAction = new Seduce(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, seduceScriptable);
        ActionTemplate(seduceScriptable, seduceAction);
    }

    public async void Counter()
    {
        GetPlayerStats();
        ImprovedActionStat counterScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "Counter");
        ICommand counterAction = new Counter(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, counterScriptable);
        ActionTemplate(counterScriptable, counterAction);
    }

    public async void FearTacticts()
    {
        GetPlayerStats();
        ActionStat fearTactictsScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "FearTacticts");
        ICommand fearTactictsAction = new FearTacticts(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, fearTactictsScriptable);
        ActionTemplate(fearTactictsScriptable, fearTactictsAction);
    }
    public async void CaptivatingPerformance()
    {
        GetPlayerStats();
        ActionStat captivatingPerformanceScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "CaptivatingPerformance");
        ICommand captivatingPerformanceAction = new CaptivatingPerformance(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, captivatingPerformanceScriptable);
        ActionTemplate(captivatingPerformanceScriptable, captivatingPerformanceAction);
    }
    public async void KeenSenses()
    {
        GetPlayerStats();
        ActionStat keenSensesScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "KeenSenses");
        ICommand keenSensesAction = new KeenSenses(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, keenSensesScriptable);
        ActionTemplate(keenSensesScriptable, keenSensesAction);
    }

    public async void ThirdRatePerformance()
    {
        GetPlayerStats();
        ActionStat thirdRatePerformanceScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "ThirdRatePerformance");
        ICommand thirdRateperformanceAction = new ThirdRatePerformance(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, thirdRatePerformanceScriptable);
        ActionTemplate(thirdRatePerformanceScriptable, thirdRateperformanceAction);
    }

    public async void Block()
    {
        Debug.Log("block done");
        GetPlayerStats();

        ImprovedActionStat blockScriptable = DAOScriptableObject.instance.GetImprovedActionData(StringData.directory, "Block");
        ICommand blockAction = new Block(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, blockScriptable);
        ActionTemplate(blockScriptable, blockAction);
    }

    public async void Dodge()
    {

        GetPlayerStats();
        ActionStat dodgeScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "Dodge");
        ICommand dodgeAction = new Dodge(playerAttacker, targetDefender, currentStatPlayer, currentStatTarget, dodgeScriptable);
        ActionTemplate(dodgeScriptable, dodgeAction);

    }




    public void ActionTemplate(ActionStat actionScriptable, ICommand tobePerformedAction)
    {


        playerInfo = currentStatPlayer;

        if (ActionResolver.instance.APResolver(playerInfo.CurrentAP, actionScriptable.APCost) >= 0)
        {

            playerInfo.CurrentAP = ActionResolver.instance.APResolver(playerInfo.CurrentAP, actionScriptable.APCost);
            int previousPV = 20;
            Turn turn = new Turn(playerAttacker, tobePerformedAction, previousPV + actionScriptable.PriorityValue);
            int rpOfCurrentPlayer;
            rpOfCurrentPlayer = playerInfo.CurrentResolve;
            if (rpOfCurrentPlayer == 0)
            {
                int randomChanceOfAction = Random.Range(0, 2);
                if (randomChanceOfAction == 0)
                {
                    // notification of rp 0
                    UI.instance.SendNotification("Player Rp is Zero");
                    Debug.Log("rp is zero");
                }
                else
                {
                    HandleTurnNew.instance.AddTurn(turn);
                    Debug.Log("else 1");
                }
            }
            else
            {
                HandleTurnNew.instance.AddTurn(turn);
                Debug.Log("else 2");
            }

            //actionNotification.gameObject.SetActive(true);
            //actionNotification.AnimateNotification(turn.Player +" Selected "+ turn.Command.GetActionName() + " Target " + turn.target);
            // actionNotification.TextEdit(turn.Player + " Selected " + turn.Command.GetActionName() + " Target " + turn.target);

        }
        else
        {
            UI.instance.SendNotification("No AP left");
        }
        // PlayerStatUI.instance.GetPlayerStatSummary(playerAttacker);
        //PlayerStatUI.instance.GetPlayerStatDetails(playerAttacker);

    }

    public void ActionTemplate(ImprovedActionStat actionScriptable, ICommand tobePerformedAction)
    {


        playerInfo = currentStatPlayer;

        if (ActionResolver.instance.APResolver(playerInfo.CurrentAP, actionScriptable.APCost) >= 0)
        {
            ButtonStackManager.instance.OnButtonPressed(actionScriptable.actionIcon);

            playerInfo.CurrentAP = ActionResolver.instance.APResolver(playerInfo.CurrentAP, actionScriptable.APCost);
            int previousPV =25;
           

            Turn turn = new Turn(playerAttacker, tobePerformedAction, previousPV + actionScriptable.PriorityValue);
            int rpOfCurrentPlayer;//rp not in use ===Date 23.09.24===
            rpOfCurrentPlayer = playerInfo.CurrentResolve;
            if (rpOfCurrentPlayer == 0)
            {
                int randomChanceOfAction = Random.Range(0, 2);
                if (randomChanceOfAction == 0)
                {
                    // notification of rp 0
                    UI.instance.SendNotification("Player Rp is Zero");
                    Debug.Log("rp is zero");
                    isTurnAdded = false;
                }
                else
                {
                    HandleTurnNew.instance.AddTurn(turn);
                    Debug.Log("else 1");
                    isTurnAdded = true;
                   
                }
            } //rp not in use ===Date 23.09.24===
            else
            {
                HandleTurnNew.instance.AddTurn(turn);
                isTurnAdded  = true;
        
            }

            //actionNotification.gameObject.SetActive(true);
            //actionNotification.AnimateNotification(turn.Player +" Selected "+ turn.Command.GetActionName() + " Target " + turn.target);
            // actionNotification.TextEdit(turn.Player + " Selected " + turn.Command.GetActionName() + " Target " + turn.target);

        }
        else
        {
            UI.instance.SendNotification("No AP left");
        }
        // PlayerStatUI.instance.GetPlayerStatSummary(playerAttacker);
        //PlayerStatUI.instance.GetPlayerStatDetails(playerAttacker);

    }


    void SendNotification(string action)
    {
        //actionNotification.StartImageAnimation(action);
    }



    public void AddTurnToAllTurns()
    {
        HandleTurnNew.instance.AddTurn(TurnManager.currentTurn);
        
    }







    public async void Ultimate()
    {
        GetPlayerStats();
        UltimateSystem._instance.useUltimate(playerAttacker,currentStatPlayer);
    }

    public async void Move()
    {
        Debug.Log("ASE");
        TurnManager.instance.ResetTargetHIghlightVisual();
        GridMovement.instance.ResetHighlightedPath();
        TurnManager.instance.targetsInRange.Clear();
        TurnManager.instance.nonCharacterTargetsInRange.Clear();
        UI.instance.ClearTargetList();
        GetPlayerStats();
        if (currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn == true)
        {

            currentStatPlayer.gameObject.GetComponent<TemporaryStats>().AutoMove = false;
            currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn = false;
            Cursor.lockState = CursorLockMode.Locked;

            ActionStat moveScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "Move");
            ButtonStackManager.instance.OnButtonPressed(moveScriptable.actionIcon);
            foreach(var gridIp in gridInput)
            {
                gridIp.enabled = true;
            }
           
           // GridMovement.instance.InAdjacentMatrix(currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition,TeamName.NullTeam, currentStatPlayer.CurrentDex,Color.green);
            GridMovement.instance.InAdjacentMatrix(currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition,TeamName.NullTeam, moveScriptable.ActionRange,Color.green);
            GridMovement.instance.setMoveParam(moveScriptable, moveScriptable.ActionRange, currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, playerAttacker.gameObject.GetComponent<NavMeshAgent>());
           // GridMovement.instance.setMoveParam(moveScriptable, currentStatPlayer.CurrentDex, currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, playerAttacker.gameObject.GetComponent<NavMeshAgent>());

            TempManager.instance.ChangeGameState(GameStates.MovementGridSelectionTurn);
        }
        else
        {
            UI.instance.SendNotification("Can't Move Now");
            TempManager.instance.ChangeGameState(GameStates.MidTurn);
        }
    }

    public async void Dash()
    {
        Debug.Log("Ap lagbe");
        TurnManager.instance.ResetTargetHIghlightVisual();
        GridMovement.instance.ResetHighlightedPath();
        TurnManager.instance.targetsInRange.Clear();
        TurnManager.instance.nonCharacterTargetsInRange.Clear();
        UI.instance.ClearTargetList();
        GetPlayerStats();
        if (currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn == true)
        {
            ActionStat dashScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "Dash");

            if (ActionResolver.instance.APResolver(currentStatPlayer.CurrentAP, dashScriptable.APCost) >= 0)
            {


                currentStatPlayer.gameObject.GetComponent<TemporaryStats>().AutoMove = false;
                currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn = false;
                Cursor.lockState = CursorLockMode.Locked;

              
          
                currentStatPlayer.CurrentAP = ActionResolver.instance.APResolver(currentStatPlayer.CurrentAP, dashScriptable.APCost);//ap cost
                ButtonStackManager.instance.OnButtonPressed(dashScriptable.actionIcon);
                foreach (var gridIp in gridInput)
                {
                    gridIp.enabled = true;
                }
                GridMovement.instance.InAdjacentMatrix(currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, TeamName.NullTeam, currentStatPlayer.CurrentDex, Color.green);
                GridMovement.instance.setMoveParam(dashScriptable, currentStatPlayer.CurrentDex, currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, playerAttacker.gameObject.GetComponent<NavMeshAgent>());

                TempManager.instance.ChangeGameState(GameStates.MovementGridSelectionTurn);
            }
            else
            {
                UI.instance.SendNotification("No AP!!Can't Dash Now");
            }
               
          
        }
        else
        {
            UI.instance.SendNotification("Can't Dash Now");
            TempManager.instance.ChangeGameState(GameStates.MidTurn);
        }
    }

    public async void WarpSurge()
    {
        Debug.Log("Ap lagbe");
        TurnManager.instance.ResetTargetHIghlightVisual();
        GridMovement.instance.ResetHighlightedPath();
        TurnManager.instance.targetsInRange.Clear();
        TurnManager.instance.nonCharacterTargetsInRange.Clear();
        UI.instance.ClearTargetList();
        GetPlayerStats();
        if (currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn == true)
        {

            currentStatPlayer.gameObject.GetComponent<TemporaryStats>().AutoMove = false;
            currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn = false;
            Cursor.lockState = CursorLockMode.Locked;

            ActionStat moveScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "WarpSurge");
            ButtonStackManager.instance.OnButtonPressed(moveScriptable.actionIcon);
            foreach (var gridIp in gridInput)
            {
                gridIp.enabled = true;
            }
            GridMovement.instance.InAdjacentMatrix(currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, TeamName.NullTeam, currentStatPlayer.CurrentDex, Color.green);
            GridMovement.instance.setMoveParam(moveScriptable, currentStatPlayer.CurrentDex, currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, playerAttacker.gameObject.GetComponent<NavMeshAgent>());

            TempManager.instance.ChangeGameState(GameStates.MovementGridSelectionTurn);
        }
        else
        {
            UI.instance.SendNotification("Can't Warp Now");
            TempManager.instance.ChangeGameState(GameStates.MidTurn);
        }
    }


    public async void GroundBlast()
    {
        Debug.Log("Grpund Blast in");
        TurnManager.instance.ResetTargetHIghlightVisual();
        GridMovement.instance.ResetHighlightedPath();
        TurnManager.instance.targetsInRange.Clear();
        TurnManager.instance.nonCharacterTargetsInRange.Clear();
        UI.instance.ClearTargetList();
        GetPlayerStats();
        if (currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn == true)
        {
            Debug.Log("Ground Ap lagbe");
            currentStatPlayer.gameObject.GetComponent<TemporaryStats>().AutoMove = false;
            currentStatPlayer.gameObject.GetComponent<PlayerTurn>().isMoveOn = false;
            Cursor.lockState = CursorLockMode.Locked;

            ActionStat moveScriptable = DAOScriptableObject.instance.GetActionData(StringData.directory, "GroundBlast");
            ButtonStackManager.instance.OnButtonPressed(moveScriptable.actionIcon);
            foreach (var gridIp in gridInput)
            {
                gridIp.enabled = true;
            }
            GridMovement.instance.InAdjacentMatrix(currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, TeamName.NullTeam, currentStatPlayer.CurrentDex, Color.green);
            GridMovement.instance.setMoveParam(moveScriptable, currentStatPlayer.CurrentDex, currentStatPlayer.gameObject.GetComponent<TemporaryStats>().currentPlayerGridPosition, playerAttacker.gameObject.GetComponent<NavMeshAgent>());

            TempManager.instance.ChangeGameState(GameStates.MovementGridSelectionTurn);
        }
        else
        {
            UI.instance.SendNotification("Can't Warp Now");
            TempManager.instance.ChangeGameState(GameStates.MidTurn);
        }
    }

    public List<ImprovedActionStat> GetMeleeActions(List<ImprovedActionStat> actions)
    {
        List<ImprovedActionStat> availableActions = new List<ImprovedActionStat>();
        foreach (ImprovedActionStat action in actions)
        {
            if (action.actionType == ActionType.Melee)
            {
                availableActions.Add(action);
            }
        }
        return availableActions;
    }

    public List<ImprovedActionStat> GetRangedActions(List<ImprovedActionStat> actions)
    {
        List<ImprovedActionStat> availableActions = new List<ImprovedActionStat>();
        foreach (ImprovedActionStat action in actions)
        {
            if (action.actionType == ActionType.Ranged)
            {
                availableActions.Add(action);
            }
        }
        return availableActions;
    }

    public List<ImprovedActionStat> GetDefenceActions(List<ImprovedActionStat> actions)
    {
        List<ImprovedActionStat> availableActions = new List<ImprovedActionStat>();
        foreach (ImprovedActionStat action in actions)
        {
            if (action.actionStance == ActionStance.Defense)
            {
                availableActions.Add(action);
            }
        }
        return availableActions;
    }
    public List<ImprovedActionStat> GetOffenseActions(List<ImprovedActionStat> actions)
    {
        List<ImprovedActionStat> availableActions = new List<ImprovedActionStat>();
        foreach (ImprovedActionStat action in actions)
        {
            if (action.actionStance == ActionStance.Offense)
            {
                availableActions.Add(action);
            }
        }
        return availableActions;
    }

    public List<ImprovedActionStat> GetActionsWithinAP(List<ImprovedActionStat> actions, TemporaryStats playerStats)
    {
        List<ImprovedActionStat> availableActions = new List<ImprovedActionStat>();
        foreach (ImprovedActionStat action in actions)
        {
            if (action.APCost <= playerStats.CurrentAP)
            {
                availableActions.Add(action);
            }
        }
        return availableActions;
    }


}
