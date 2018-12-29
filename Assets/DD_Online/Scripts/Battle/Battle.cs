using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { HeroSelection, SkillUsing, PlayerTurn, AITurn }

public class Battle : MonoBehaviour
{
    public Transform[] skillPoints1;
    public Transform[] skillPoints2;
    private GameObject[,] skillPrefabs;

    public Transform[] heroPoints1;
    public Transform[] heroPoints2;
    private GameObject[] heroPrefabs;

    public List<Hero> heroesTeam1;
    public List<Hero> heroesTeam2;
    private List<Hero> heroes;

    private Hero currentHero;
    private int playerID1;
    private int playerID2;

    // Battle sequence
    private BattleState battleState;



    void Start()
    {
        battleState = BattleState.HeroSelection;

        if (checkOwners(heroesTeam1, heroesTeam2) == false)
            Debug.LogError("checkOwners failed!");

        createSkillPrefabs();
        createHeroesPrefabs();
    }

    void Update()
    {
        if (battleState == BattleState.HeroSelection)
        {
            currentHero = selectHero(heroes, currentHero);
            foreach (Hero hero in heroesTeam1) HeroSelector.setSelector(hero, 1);
            foreach (Hero hero in heroesTeam2) HeroSelector.setSelector(hero, 2);

            int index = 0;
            foreach (Skill skill in currentHero.skillList)
            {
                int team = -1;
                if (currentHero.ownerID == playerID1) team = 0;
                else if (currentHero.ownerID == playerID2) team = 1;

                skillPrefabs[team,index].GetComponent<SpriteRenderer>().sprite =
                    SkillSpritesContainer.getSprite("Skill_" + skill.skillName.ToString());

                index++;
            }

            battleState = BattleState.PlayerTurn;
        }
        else if (battleState == BattleState.PlayerTurn)
        {
            // enable skill switching
            // target selection
            if (Input.GetKeyDown(KeyCode.Space))
                battleState = BattleState.SkillUsing;
        }
        else if (battleState == BattleState.SkillUsing)
        {


            battleState = BattleState.HeroSelection;
        }
    }





    private Hero selectHero(List<Hero> heroList, Hero currentHero)
    {
        if (heroList.Count == 0)
        {
            Debug.Log("selectHero | heroList.Count == 0");
            return null;
        }

        if (currentHero != null && currentHero.turnState == TurnState.InProcess)
            currentHero.turnState = TurnState.Done;

        Hero nextHero = new Hero();
        int speed = -100;

        foreach (Hero hero in heroList)
        {
            if (hero.turnState == TurnState.Undone)
            {
                if (hero.speed >= speed)
                {
                    nextHero = hero;
                    speed = hero.speed;
                }
            }
        }

        if (nextHero == null)
        {
            Debug.Log("NULL");
            startNewRound(heroList, currentHero);
            nextHero = selectHero(heroList, currentHero);
        }
        else nextHero.turnState = TurnState.InProcess;

        return nextHero;
    }

    private void startNewRound(List<Hero> heroList, Hero currentHero)
    {
        foreach (Hero hero in heroList)
        {
            hero.turnState = TurnState.Undone;
          //  currentHero = null;
        }
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
        skillPrefabs = new GameObject[2,4];
        for (int i = 0; i < 4; ++i)
        {
            skillPrefabs[0,i] = Instantiate(Resources.Load("Prefabs/SkillPrefab") as GameObject,
                   skillPoints1[i].position, Quaternion.identity) as GameObject;
            skillPrefabs[1,i] = Instantiate(Resources.Load("Prefabs/SkillPrefab") as GameObject,
                   skillPoints2[i].position, Quaternion.identity) as GameObject;
        }
    }

    private void createHeroesPrefabs()
    {
        heroPrefabs = new GameObject[8];
        heroes = new List<Hero>();
        foreach (Hero hero in heroesTeam1)
        {
            heroPrefabs[hero.position - 1] = Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + hero.heroClass.ToString()) as GameObject,
                   heroPoints1[hero.position - 1].position, Quaternion.identity) as GameObject;

            heroes.Add(hero);
        }
        foreach (Hero hero in heroesTeam2)
        {
            heroPrefabs[hero.position - 1 + 4] = Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + hero.heroClass.ToString()) as GameObject,
                   heroPoints2[hero.position - 1].position, Quaternion.identity) as GameObject;

            Vector3 rotation = new Vector3(0, 180, 0);
            heroPrefabs[hero.position - 1 + 4].transform.eulerAngles = rotation;

            heroes.Add(hero);
        }
    }
}
