using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState_TeamBuilding : GameState
{
    public List<TeamBuildingHero> heroes;


    public GameState_TeamBuilding(Game game)
    : base(game)
    {
        List<Hero> loadedHeroes = DataStore.firstLoadHeroes();
        heroes = new List<TeamBuildingHero>();
        foreach (var hero in loadedHeroes)
        {
            heroes.Add(new TeamBuildingHero(hero));
        }

        instantiateHeroes();
    }





    public override void update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SceneManager.LoadScene(1, LoadSceneMode.Single);
        //   // game.gameState = new GameState_Battle(game, heroes);
        //}



    }



    private void instantiateHeroes()
    {
        foreach (var hero in heroes)
            hero.instantiate();
    }

    private void updateContainerLength()
    {
        
    }

    private void onEndDrag()
    {
       
    }

    private void onClick()
    {

    }
}
