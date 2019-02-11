using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// TODO: Delete MonoBehaviour Inheritance
public class Hero
{
    public Hero()
    {
        TurnState = TurnStates.Undone;
    }


    public HeroClass name;

    [SerializeField]
    protected int health;
    public int maxHealth;
    protected bool isSelected;
    public int speed;
    public Team team;
    public int position;
    public List<SkillName> skillNames;
    //public int ownerID;
    [HideInInspector]
    public List<Skill> skillList;
    [HideInInspector]
    public GameObject prefab;

    [HideInInspector]
    public Team oppositeTeam;
    [HideInInspector]
    protected TurnStates turnState;
    [HideInInspector]
    public Image healthBar;
    [HideInInspector]
    public BoxCollider2D collider;
    [HideInInspector]
    public GameObject turnDoneIndicator;
    [HideInInspector]
    public GameObject turnInProcessIndicator;
    [HideInInspector]
    public GameObject selector;
    [HideInInspector]
    public bool isTargeted;



    public TurnStates TurnState
    {
        get
        {
            return turnState;
        }
        set
        {
            turnState = value;
            //setTurnDoneIndicator(turnState);
            //setTurnInProcessIndicator(turnState);
        }
    }

    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;

            //setSelector(isSelected);
            //if (isSelected == true)
            //    instantiateSkills();
            //else
            //    destroySkills();
        }
    }

    public int Health {
        get
        {
            return health;
        }
        set
        {
            health = value;
            //health = Mathf.Clamp(health, 0, maxHealth);
            //updateHealthBar();
        }
    }

    private void Awake()
    {
        Debug.Log("!Awake Hero");
        //TurnState = TurnStates.Undone;

        //foreach (SkillName skillName in skillNames)
        //{
        //    Skill skill = SkillContainer.Instance.getSkill(heroClass, skillName);
        //    skill.setOwner(this);
        //    skillList.Add(skill);
        //}
        //addSkipTurnSkill();

        //if (team == Team.Left)
        //    oppositeTeam = Team.Right;
        //else if (team == Team.Right)
        //    oppositeTeam = Team.Left;

        //maxHealth = health;
    }

    protected virtual void addSkipTurnSkill()
    {
    //    Skill skill = new Skill();
    //    skill.setOwner(this);
    //    skill.skillName = SkillName.SkipTurn;
    //    skill.usePositions = new List<int>() { 1,2,3,4 };
    //    skillList.Add(skill);
    }



    protected virtual void instantiateSkills()
    {
        //int index = 0;

        //if (turnState == TurnStates.InProcess)
        //{
        //    foreach (Skill skill in skillList)
        //    {
        //        skill.instantiate(SceneElementsContainer.skillTransforms[(int)team][index++].position);
        //        skill.enable(checkSkillUsePosition(skill));
        //    }
        //}
        //else
        //{
        //    foreach (Skill skill in skillList)
        //    {
        //        if (skill.skillName != SkillName.SkipTurn)
        //        {
        //            skill.instantiate(SceneElementsContainer.skillTransforms[(int)team][index++].position);
        //            skill.enable(false);
        //        }
        //    }
        //}
    }

    protected virtual bool checkSkillUsePosition(Skill skill)
    {
    //    foreach (int position in skill.usePositions)
    //    {
    //        if (position == this.position)
    //            return true;
    //    }

        return false;
    }



    public void destroySkills()
    {
        foreach (Skill skill in skillList)
            skill.destroy();
    }

    public void enableSkills(bool flag)
    {
        foreach (Skill skill in skillList)
            skill.enable(flag);
    }




    protected virtual void setSelector(bool flag) {}
    protected virtual void setTurnDoneIndicator(TurnStates turnState) {}
    protected virtual void enableTurnDoneIndicator(bool flag) {}
    protected virtual void setTurnInProcessIndicator(TurnStates turnState) {}
    protected virtual void enableTurnInProcessIndicator(bool flag) {}



    public virtual void instantiate()
    {
        //if (prefab == null)
        //{
        //    prefab = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + heroClass.ToString()) as GameObject,
        //            SceneElementsContainer.heroTransforms[(int)team][position - 1].position, Quaternion.identity) as GameObject;

        //    if (team == Team.Right)
        //    {
        //        Vector3 rotation = new Vector3(0, 180, 0);
        //        prefab.transform.eulerAngles = rotation;
        //    }

        //    collider = SceneElementsContainer.heroColliders[(int)team][position - 1];
        //    //GameObject obj = prefab.GetComponent<>
        //    RectTransform[] objects = prefab.GetComponentsInChildren<RectTransform>();
        //    foreach (RectTransform obj in objects)
        //    {
        //        if (obj.name == "HealthBar")
        //            healthBar = obj.GetComponent<Image>();
        //    }
        //}
    }
}