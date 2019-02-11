using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_StartBattle : BattleState
{
    public BattleState_StartBattle(GameState_Battle battle)
           : base(battle)
    { }

    public override void execute()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Debug.Log("Q");
        Debug.Log("!0");
        //heroes = battle.heroes;
        //battle.heroes = SceneElementsContainer.heroes;
        createHeroes();
        Debug.Log("!1");
        createHeroSelectorPrefabs();
        Debug.Log("!2");
        createSkillSelectorPrefab();
        Debug.Log("!3");
        createTurnInProcessIndicatorPrefab();
        Debug.Log("!4");

        battle.battleState = new BattleState_HeroSelection(battle, this);
        Debug.Log("!5");
    }

    

    private void createHeroes()
    {
        foreach (var hero in battle.heroes)
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

        foreach (var hero in battle.heroes)
            hero.selector = selectors[(int)hero.team];
    }

    private void createTurnInProcessIndicatorPrefab()
    {
        GameObject indicator;

        indicator = MonoBehaviour.Instantiate(Resources.Load("Prefabs/TurnInProcessIndicatorPrefab") as GameObject,
               Vector3.zero, Quaternion.identity) as GameObject;
        indicator.GetComponent<SpriteRenderer>().enabled = false;

        foreach (var hero in battle.heroes)
            hero.turnInProcessIndicator = indicator;
    }

    private void createSkillSelectorPrefab()
    {
        skillSelector = MonoBehaviour.Instantiate(Resources.Load("Prefabs/SkillSelectorPrefab") as GameObject,
               Vector3.zero, Quaternion.identity) as GameObject;
        skillSelector.GetComponent<SpriteRenderer>().enabled = false;
    }
}