using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    //public Transform[] skillPoints1;
    //public Transform[] skillPoints2;
    //public Transform[][] skillPoints;

    //public Transform[] heroPoints1;
    //public Transform[] heroPoints2;

    //public BoxCollider2D[] heroColliders1;
    //public BoxCollider2D[] heroColliders2;

    //public Transform[] turnStateSelectorPoints1;
    //public Transform[] turnStateSelectorPoints2;

    //public List<Hero> heroesTeam1;
    //public List<Hero> heroesTeam2;

    private GameObject skillSelector;

    [HideInInspector]
    public List<Hero> heroes;

    [HideInInspector]
    public Hero currentHero;
    [HideInInspector]
    public int playerID1;
    [HideInInspector]
    public int playerID2;

    public BattleState battleState;



    void Start()
    {
        heroes = SceneElementsContainer.heroes;
        createHeroes();
        createHeroSelectorPrefabs();
        createSkillSelectorPrefab();
        createTurnInProcessIndicatorPrefab();

        battleState = new BattleState_HeroSelection(this);
    }

    void Update()
    {
        battleState.execute();
    }



    public void selectHero(Hero hero)
    {
        foreach (Hero h in heroes)
        {
            if (h.team == hero.team)
                h.setSelected(false);
        }
        hero.setSelected(true);
    }

    public void selectSkill(Skill skill, bool enableFlag = true)
    {
        if (enableFlag == true)
        {
            foreach (Skill s in skill.owner.skillList)
                s.setSelected(false);
        }
      
        skill.setSelected(enableFlag);
        setSkillSelector(skill, enableFlag);
    }

    private void setSkillSelector(Skill skill, bool flag)
    {
        skillSelector.transform.position = skill.prefab.transform.position;
        skillSelector.GetComponent<SpriteRenderer>().enabled = flag;
    }



    private void createHeroes()
    {
        foreach (Hero hero in heroes)
            hero.instantiate();
    }

    private void createHeroSelectorPrefabs()
    {
        GameObject[] selectors = new GameObject[2];

        for (int i = 0; i < 2; i++)
        {
            selectors[i] = MonoBehaviour.Instantiate(Resources.Load("Prefabs/PointerSelectorPrefab") as GameObject,
                   Vector3.zero, Quaternion.identity) as GameObject;
            selectors[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        foreach (Hero hero in heroes)
            hero.selector = selectors[(int)hero.team];
    }

    private void createTurnInProcessIndicatorPrefab()
    {
        GameObject indicator;

        indicator = MonoBehaviour.Instantiate(Resources.Load("Prefabs/TurnInProcessIndicatorPrefab") as GameObject,
               Vector3.zero, Quaternion.identity) as GameObject;
        indicator.GetComponent<SpriteRenderer>().enabled = false; 

        foreach (Hero hero in heroes)
            hero.turnInProcessIndicator = indicator;
    }

    private void createSkillSelectorPrefab()
    {
        skillSelector = MonoBehaviour.Instantiate(Resources.Load("Prefabs/SkillSelectorPrefab") as GameObject,
               Vector3.zero, Quaternion.identity) as GameObject;
        skillSelector.GetComponent<SpriteRenderer>().enabled = false;
    }
}
