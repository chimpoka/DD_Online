using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Battle : GameState
{
    public GameState_Battle(Game game, List<Hero> heroes)
    : base(game)
    {
        this.heroes = new List<BattleHero>();

        foreach (Hero hero in heroes)
            this.heroes.Add(new BattleHero(hero));

        Debug.Log("!!!1");

        //foreach (Hero hero in heroes)
        //    Debug.Log("1: " + hero.heroClass + ", " + hero.position);

        battleState = new BattleState_StartBattle(this);
        Debug.Log("!!!2");
    }

    public List<BattleHero> heroes;
    public BattleState battleState;

    public override void update()
    {
        battleState.execute();
        battleState.showSkillHover();
    }
}