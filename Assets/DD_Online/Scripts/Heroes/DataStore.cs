using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStore
{
    static public List<Hero> loadHeroes()
    {
        List<Hero> heroes = new List<Hero>();

       // if (PlayerPrefs.GetInt("FirstPlay", 1) == 1)
          //  return firstLoadHeroes();
        //else
        {
            for (int i = 0; i < PlayerPrefs.GetInt("HeroesCount", 16); i++)
            {
                Hero hero = SkillContainer.Instance.createHero((HeroClass)PlayerPrefs.GetInt("Name" + i, 0));
                hero.team = (Team)PlayerPrefs.GetInt("Team" + i, -1);
                heroes.Add(hero);
            }
        }
        return heroes;
    }

    static public void saveHeroes(List<Hero> heroes)
    {
        PlayerPrefs.SetInt("FirstPlay", 0);
        PlayerPrefs.SetInt("HeroesCount", heroes.Count);

        for (int i = 0; i < heroes.Count; i++)
        {
            PlayerPrefs.SetInt("Name" + i, (int)heroes[i].name);
            PlayerPrefs.SetInt("Team" + i, (int)heroes[i].team);
        }
    }

    static public List<Hero> firstLoadHeroes()
    {
        List<Hero> heroes = new List<Hero>();

        Hero hero = SkillContainer.Instance.createHero(HeroClass.Berserk); hero.team = Team.Left; hero.position = 1; heroes.Add(hero);
        hero = SkillContainer.Instance.createHero(HeroClass.Berserk); hero.team = Team.Left; hero.position = 2; heroes.Add(hero);
        hero = SkillContainer.Instance.createHero(HeroClass.Priest); hero.team = Team.Left; hero.position = 3; heroes.Add(hero);
        hero = SkillContainer.Instance.createHero(HeroClass.Priest); hero.team = Team.Left; hero.position = 4; heroes.Add(hero);
        hero = SkillContainer.Instance.createHero(HeroClass.Mage); hero.team = Team.Right; hero.position = 1; heroes.Add(hero);
        hero = SkillContainer.Instance.createHero(HeroClass.Mage); hero.team = Team.Right; hero.position = 2; heroes.Add(hero);
        hero = SkillContainer.Instance.createHero(HeroClass.Fairy); hero.team = Team.Right; hero.position = 3; heroes.Add(hero);
        hero = SkillContainer.Instance.createHero(HeroClass.Fairy); hero.team = Team.Right; hero.position = 4; heroes.Add(hero);

        //Hero hero = SkillContainer.Instance.createHero(HeroClass.Berserk); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Berserk); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Priest); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Priest); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Mage); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Mage); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Fairy); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Fairy); hero.team = Team.Left; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Berserk); hero.team = Team.Right; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Berserk); hero.team = Team.Right; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Priest); hero.team = Team.Right; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Priest); hero.team = Team.Right; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Mage); hero.team = Team.Right; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Mage); hero.team = Team.Right; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Fairy); hero.team = Team.Right; heroes.Add(hero);
        //hero = SkillContainer.Instance.createHero(HeroClass.Fairy); hero.team = Team.Right; heroes.Add(hero);

        return heroes;
    }

    //static public List<TeamBuildingHero> firstLoadHeroes()
    //{
    //    List<TeamBuildingHero> heroes = new List<TeamBuildingHero>();
    //    TeamBuildingHero hero = new TeamBuildingHero(); hero.name = HeroClass.Berserk; heroes.Add(hero);
    //    hero = new TeamBuildingHero(); hero.name = HeroClass.Berserk; heroes.Add(hero);
    //    hero = new TeamBuildingHero(); hero.name = HeroClass.Mage; heroes.Add(hero);
    //    hero = new TeamBuildingHero(); hero.name = HeroClass.Mage; heroes.Add(hero);
    //    hero = new TeamBuildingHero(); hero.name = HeroClass.Fairy; heroes.Add(hero);
    //    hero = new TeamBuildingHero(); hero.name = HeroClass.Fairy; heroes.Add(hero);
    //    hero = new TeamBuildingHero(); hero.name = HeroClass.Priest; heroes.Add(hero);
    //    hero = new TeamBuildingHero(); hero.name = HeroClass.Priest; heroes.Add(hero);

    //    return heroes;
    //}
}
