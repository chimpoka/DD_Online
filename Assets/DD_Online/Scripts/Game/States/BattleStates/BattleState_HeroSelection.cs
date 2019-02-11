using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_HeroSelection : BattleState
{
    public BattleState_HeroSelection(GameState_Battle battle, BattleState state)
        : base(battle, state)
    {}

    public override void execute()
    {
        Debug.Log("!5");
        if (selectedSkill != null)
            selectSkill(selectedSkill, false);
        Debug.Log("!6");
        foreach (Hero hero in battle.heroes)
            hero.IsSelected = false;
        Debug.Log("!7");
        //currentHero = battle.heroes[3];
        //Debug.Log("heroes[3]: " + currentHero.heroClass + ", " + currentHero.position);
        currentHero = updateTurnStates();
        Debug.Log("!8");
        selectHero(currentHero);
        Debug.Log("!9");
        battle.battleState = new BattleState_TurnBeforeSkillSelected(battle, this);
        Debug.Log("!10");
    }



    private BattleHero updateTurnStates()
    {
        if (currentHero == null) currentHero = new BattleHero();
        else currentHero.TurnState = TurnStates.Done;

        BattleHero nextHero = new BattleHero();
        int speed = -100;

        foreach (var hero in battle.heroes)
        {
            if (hero.TurnState == TurnStates.Undone)
            {
                if (hero.speed >= speed)
                {
                    nextHero = hero;
                    speed = hero.speed;
                }
            }
        }

        //if (nextHero == null)
        //{
        //    Debug.Log("New Round");
        //    startNewRound();
        //    nextHero = updateTurnStates();
        //}
        //else nextHero.TurnState = TurnStates.InProcess;
        nextHero.TurnState = TurnStates.InProcess;

        return nextHero;
    }

    private void startNewRound()
    {
        foreach (var hero in battle.heroes)
            hero.TurnState = TurnStates.Undone;
    }

}