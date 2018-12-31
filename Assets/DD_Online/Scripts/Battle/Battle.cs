using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Transform[] skillPoints1;
    public Transform[] skillPoints2;
    public Transform[][] skillPoints;
    [HideInInspector]
    public GameObject[][] skillPrefabs;
    public GameObject[] skillSelectorPrefabs;

    public Transform[] heroPoints1;
    public Transform[] heroPoints2;
    [HideInInspector]
    public GameObject[][] heroPrefabs;

    public BoxCollider2D[] heroColliders1;
    public BoxCollider2D[] heroColliders2;

    public Transform[] pointerSelectorPoints1;
    public Transform[] pointerSelectorPoints2;
    public Transform[][] selectedHeroPoints;
    public GameObject[] heroSelectorPrefabs;

    public Transform[] turnStateSelectorPoints1;
    public Transform[] turnStateSelectorPoints2;

    public List<Hero> heroesTeam1;
    public List<Hero> heroesTeam2;
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
        //if (checkOwners(heroesTeam1, heroesTeam2) == false)
        //    Debug.LogError("checkOwners failed!");

        //createSelectedHeroPoints();
        //createSkillPrefabs();
        //createHeroesPrefabs();

        createHeroes();
        setSkillPoints();
        battleState = new BattleState_HeroSelection(this);
    }

    void Update()
    {
        battleState.execute();
    }



    private bool checkOwners(List<Hero> heroes1, List<Hero> heroes2)
    {
        playerID1 = heroes1[0].ownerID;
        playerID2 = heroes2[0].ownerID;
        
        foreach (Hero hero in heroes1)
        {
            if (hero.ownerID != playerID1)
            {
                Debug.LogError("Heroes in team1 has different ownerID: " + playerID1 + ", " + hero.ownerID);
                return false;
            }
        }

        foreach (Hero hero in heroes2)
        {
            if (hero.ownerID != playerID2)
            {
                Debug.LogError("Heroes in team1 has different ownerID: " + playerID2 + ", " + hero.ownerID);
                return false;
            }
        }

        return true;
    }



    public void selectHero(Hero hero)
    {
        foreach (Hero h in heroes)
        {
            if (h.team == hero.team)
                h.enablePointerSelector(false);
        }
        hero.enablePointerSelector(true);
    }

    public void createSkills(Hero hero)
    {
        foreach (Hero h in heroes)
        {
            if (h.team == hero.team)
                h.destroySkills();
        }

        int index = 0;
        foreach (Skill skill in hero.skillList)
        {
            skill.instantiate(skillPoints[(int)hero.team][index++].position);
            skill.enable(true);
        }
    }

    protected void selectSkill(Skill skill)
    {

    }



    private void setSkillPoints()
    {
        skillPoints = new Transform[2][];
        skillPoints[0] = skillPoints1;
        skillPoints[1] = skillPoints2;
    }

    private void createHeroes()
    {
        heroes = new List<Hero>();
        foreach (Hero hero in heroesTeam1)
            heroes.Add(hero);
        foreach (Hero hero in heroesTeam2)
            heroes.Add(hero);

        Transform[][] heroTransforms = new Transform[2][];
        heroTransforms[0] = heroPoints1;
        heroTransforms[1] = heroPoints2;

        BoxCollider2D[][] heroColliders = new BoxCollider2D[2][];
        heroColliders[0] = heroColliders1;
        heroColliders[1] = heroColliders2;

        Transform[][] turnStateSelectorPoints = new Transform[2][];
        turnStateSelectorPoints[0] = turnStateSelectorPoints1;
        turnStateSelectorPoints[1] = turnStateSelectorPoints2;

        Transform[][] pointerSelectorPoints = new Transform[2][];
        pointerSelectorPoints[0] = pointerSelectorPoints1;
        pointerSelectorPoints[1] = pointerSelectorPoints2;
        
        foreach (Hero hero in heroes)
        {
            hero.instantiate(heroTransforms[(int)hero.team][hero.position - 1].position);
            hero.setCollider(heroColliders[(int)hero.team][hero.position - 1]);
            hero.createTurnStateSelector(turnStateSelectorPoints[(int)hero.team][hero.position - 1].position);
            hero.createPointerSelector(pointerSelectorPoints[(int)hero.team][hero.position - 1].position);
            hero.enablePointerSelector(false);

            if (hero.team == Hero.Team.Right)
            {
                Vector3 rotation = new Vector3(0, 180, 0);
                hero.prefab.transform.eulerAngles = rotation;
            }
        }
    }
}
