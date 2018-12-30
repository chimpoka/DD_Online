using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Transform[] skillPoints1;
    public Transform[] skillPoints2;
    [HideInInspector]
    public GameObject[][] skillPrefabs;

    public Transform[] heroPoints1;
    public Transform[] heroPoints2;
    [HideInInspector]
    public GameObject[][] heroPrefabs;

    public BoxCollider2D[] heroColliders1;
    public BoxCollider2D[] heroColliders2;

    public Transform[] selectedHeroPoints1;
    public Transform[] selectedHeroPoints2;
    public Transform[][] selectedHeroPoints;
    public GameObject[] heroSelectorPrefabs;

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
        battleState = new BattleState_HeroSelection(this);

        //if (checkOwners(heroesTeam1, heroesTeam2) == false)
        //    Debug.LogError("checkOwners failed!");

        createSelectedHeroPoints();
        createSkillPrefabs();
        createHeroesPrefabs();
    }

    void Update()
    {
        battleState.execute();
    }


    private void createSelectedHeroPoints()
    {
        selectedHeroPoints = new Transform[2][];
        selectedHeroPoints[0] = selectedHeroPoints1;
        selectedHeroPoints[1] = selectedHeroPoints2;
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



    private void createSkillPrefabs()
    {
        skillPrefabs = new GameObject[2][];
        GameObject[] prefabs1 = new GameObject[4];
        GameObject[] prefabs2 = new GameObject[4];

        for (int i = 0; i < 4; ++i)
        {
            prefabs1[i] = Instantiate(Resources.Load("Prefabs/SkillPrefab") as GameObject,
                   skillPoints1[i].position, Quaternion.identity) as GameObject;
            prefabs2[i] = Instantiate(Resources.Load("Prefabs/SkillPrefab") as GameObject,
                   skillPoints2[i].position, Quaternion.identity) as GameObject;

            //skillPrefabs[0][i] = Instantiate(Resources.Load("Prefabs/SkillPrefab") as GameObject,
            //       skillPoints1[i].position, Quaternion.identity) as GameObject;
            //skillPrefabs[1][i] = Instantiate(Resources.Load("Prefabs/SkillPrefab") as GameObject,
            //       skillPoints2[i].position, Quaternion.identity) as GameObject;
        }

        skillPrefabs[0] = prefabs1;
        skillPrefabs[1] = prefabs2;
    }



    private void createHeroesPrefabs()
    {
        heroPrefabs = new GameObject[2][];
        GameObject[] prefabs1 = new GameObject[4];
        GameObject[] prefabs2 = new GameObject[4];

        heroes = new List<Hero>();

        foreach (Hero hero in heroesTeam1)
        {
            //heroPrefabs[0][hero.position - 1]
            prefabs1[hero.position - 1] = Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + hero.heroClass.ToString()) as GameObject,
                   heroPoints1[hero.position - 1].position, Quaternion.identity) as GameObject;

            hero.collider = heroColliders1[hero.position - 1];

            int index = 0;
            foreach (Skill skill in hero.skillList)
            {
                skill.prefab = skillPrefabs[(int)hero.team][index++];
            }
           
            heroes.Add(hero);
        }

        foreach (Hero hero in heroesTeam2)
        {
            //heroPrefabs[1][hero.position - 1]
            prefabs2[hero.position - 1] = Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + hero.heroClass.ToString()) as GameObject,
                   heroPoints2[hero.position - 1].position, Quaternion.identity) as GameObject;

            Vector3 rotation = new Vector3(0, 180, 0);
            //heroPrefabs[1][hero.position - 1].transform.eulerAngles = rotation;
            prefabs2[hero.position - 1].transform.eulerAngles = rotation;

            hero.collider = heroColliders2[hero.position - 1];

            int index = 0;
            foreach (Skill skill in hero.skillList)
            {
                skill.prefab = skillPrefabs[(int)hero.team][index++];
            }

            heroes.Add(hero);
        }

        heroPrefabs[0] = prefabs1;
        heroPrefabs[1] = prefabs2;
    }

}
