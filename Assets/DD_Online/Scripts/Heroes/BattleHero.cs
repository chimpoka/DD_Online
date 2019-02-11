using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// TODO: Delete MonoBehaviour Inheritance
public class BattleHero : Hero
{
    public BattleHero()
        : base()
    {
        //Awake();
    }

    public BattleHero(Hero hero)
    {
        this.name = hero.name;
        this.maxHealth = hero.maxHealth;
        this.speed = hero.speed;
        this.team = hero.team;
        this.position = hero.position;
        this.skillList = hero.skillList;

        Awake();
    }
    //public HeroClass heroClass;

    //[SerializeField]
    //private int health;
    //private int maxHealth;
    //private bool isSelected;
    //public int speed;
    //public Team team;
    //public int position;
    //public List<SkillName> skillNames;
    ////public int ownerID;
    //[HideInInspector]
    //public List<Skill> skillList;
    //[HideInInspector]
    //public GameObject prefab;

    //[HideInInspector]
    //public Team oppositeTeam;
    //[HideInInspector]
    //private TurnStates turnState;
    //[HideInInspector]
    //public Image healthBar;
    //[HideInInspector]
    //new public BoxCollider2D collider;
    //[HideInInspector]
    //public GameObject turnDoneIndicator;
    //[HideInInspector]
    //public GameObject turnInProcessIndicator;
    //[HideInInspector]
    //public GameObject selector;
    //[HideInInspector]
    //public bool isTargeted;



    public new TurnStates TurnState
    {
        get
        {
            return turnState;
        }
        set
        {
            turnState = value;
            setTurnDoneIndicator(turnState);
            setTurnInProcessIndicator(turnState);
        }
    }

    public new bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;

            setSelector(isSelected);
            if (isSelected == true)
                instantiateSkills();
            else
                destroySkills();
        }
    }

    public new int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            health = Mathf.Clamp(health, 0, maxHealth);
            updateHealthBar();
        }
    }

    private void Awake()
    {
        Debug.Log("!Awake BattleHero");
        TurnState = TurnStates.Undone;

        //foreach (SkillName skillName in skillNames)
        //{
        //    Skill skill = SkillContainer.Instance.getSkill(name, skillName);
        //    skill.setOwner(this);
        //    skillList.Add(skill);
        //}
        //addSkipTurnSkill();

        if (team == Team.Left)
            oppositeTeam = Team.Right;
        else if (team == Team.Right)
            oppositeTeam = Team.Left;

        maxHealth = health;
    }

    //protected override void addSkipTurnSkill()
    //{
    //    Skill skill = new Skill();
    //    skill.setOwner(this);
    //    skill.skillName = SkillName.SkipTurn;
    //    skill.usePositions = new List<int>() { 1, 2, 3, 4 };
    //    skillList.Add(skill);
    //}



    protected override void instantiateSkills()
    {
        int index = 0;
        Debug.Log(TurnState);
        if (turnState == TurnStates.InProcess)
        {
            foreach (Skill skill in skillList)
            {
                skill.instantiate(SceneElementsContainer.skillTransforms[(int)team][index++].position);
                skill.enable(checkSkillUsePosition(skill));
            }
        }
        else
        {
            foreach (Skill skill in skillList)
            {
                if (skill.skillName != SkillName.SkipTurn)
                {
                    skill.instantiate(SceneElementsContainer.skillTransforms[(int)team][index++].position);
                    skill.enable(false);
                }
            }
        }
    }

    protected override bool checkSkillUsePosition(Skill skill)
    {
        foreach (int position in skill.usePositions)
        {
            if (position == this.position)
                return true;
        }

        return false;
    }



    //public void destroySkills()
    //{
    //    foreach (Skill skill in skillList)
    //        skill.destroy();
    //}

    //public void enableSkills(bool flag)
    //{
    //    foreach (Skill skill in skillList)
    //        skill.enable(flag);
    //}


    protected override void setSelector(bool flag)
    {
        if (selector != null)
        {
            if (flag == true)
                selector.transform.position = prefab.transform.position + Vector3.up * 2.5f;

            selector.GetComponent<SpriteRenderer>().enabled = flag;
        }
    }

    protected override void setTurnDoneIndicator(TurnStates turnState)
    {
        if (TurnState == TurnStates.Undone)
            enableTurnDoneIndicator(false);
        else if (TurnState == TurnStates.Done)
            enableTurnDoneIndicator(true);
        else if (TurnState == TurnStates.InProcess)
            enableTurnDoneIndicator(false);
    }

    protected override void enableTurnDoneIndicator(bool flag)
    {
        if (turnDoneIndicator == null && flag == true)
        {
            turnDoneIndicator = MonoBehaviour.Instantiate(Resources.Load("Prefabs/TurnDoneIndicatorPrefab") as GameObject,
                    SceneElementsContainer.turnStateIndicatorTransforms[(int)team][position - 1].position + Vector3.right * 0.5f, Quaternion.identity) as GameObject;
        }
        else if (turnDoneIndicator != null)
        {
            turnDoneIndicator.GetComponent<SpriteRenderer>().enabled = flag;
        }
    }

    protected override void setTurnInProcessIndicator(TurnStates turnState)
    {
        if (TurnState == TurnStates.Undone)
            enableTurnInProcessIndicator(false);
        else if (TurnState == TurnStates.Done)
            enableTurnInProcessIndicator(false);
        else if (TurnState == TurnStates.InProcess)
            enableTurnInProcessIndicator(true);
    }

    protected override void enableTurnInProcessIndicator(bool flag)
    {
        if (turnInProcessIndicator != null)
        {
            if (flag == true)
                turnInProcessIndicator.transform.position =
                    SceneElementsContainer.turnStateIndicatorTransforms[(int)team][position - 1].position;

            turnInProcessIndicator.GetComponent<SpriteRenderer>().enabled = flag;
        }
    }



    public override void instantiate()
    {
        if (prefab == null)
        {
            Debug.Log("Prefabs/Heroes/Prefab_" + name.ToString());
            //Debug.Log(team + ", " + (int)team + ", " + position);
            //Debug.Log(SceneElementsContainer.heroTransforms[(int)team][position - 1].name);
            prefab = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + name.ToString()) as GameObject,
                    SceneElementsContainer.heroTransforms[(int)team][position - 1].position, Quaternion.identity) as GameObject;

            if (team == Team.Right)
            {
                Vector3 rotation = new Vector3(0, 180, 0);
                prefab.transform.eulerAngles = rotation;
            }

            collider = SceneElementsContainer.heroColliders[(int)team][position - 1];
            //GameObject obj = prefab.GetComponent<>
            RectTransform[] objects = prefab.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform obj in objects)
            {
                if (obj.name == "HealthBar")
                    healthBar = obj.GetComponent<Image>();
            }
        }
    }

    private void updateHealthBar()
    {
        healthBar.fillAmount = Mathf.InverseLerp(0, maxHealth, health);
    }
}