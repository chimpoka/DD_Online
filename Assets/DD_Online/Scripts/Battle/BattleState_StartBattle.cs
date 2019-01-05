using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_StartBattle : BattleState
{
    public BattleState_StartBattle(Battle battle)
           : base(battle)
    { }

    public override void execute()
    {
        heroes = SceneElementsContainer.heroes;
        createHeroes();
        createHeroSelectorPrefabs();
        createSkillSelectorPrefab();
        createTurnInProcessIndicatorPrefab();

        battle.battleState = new BattleState_HeroSelection(battle, this);
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