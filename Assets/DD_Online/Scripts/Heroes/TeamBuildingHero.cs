using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBuildingHero
{
    public TeamBuildingHero() {}

    public TeamBuildingHero(Hero hero)
    {
        name = hero.name;
        team = hero.team;
    }

    public HeroClass name;
    private bool isSelected;
    public int speed;
    public Team team;
    public int position;
    public List<Skill> skillList;
    public GameObject prefab;

    protected bool IsSelected { get => isSelected; set => isSelected = value; }



    public void instantiate()
    {
        if (prefab == null)
        {
            Debug.Log("Prefabs/HeroIcons/Prefab_" + name.ToString());
            //Debug.Log(team + ", " + (int)team + ", " + position);
            //Debug.Log(SceneElementsContainer.heroTransforms[(int)team][position - 1].name);
            prefab = MonoBehaviour.Instantiate(Resources.Load("Prefabs/HeroIcons/Prefab_" + name.ToString())) as GameObject;

            //if (team == Team.Right)
            //{
            //    Vector3 rotation = new Vector3(0, 180, 0);
            //    prefab.transform.eulerAngles = rotation;
            //}

           // Debug.Log(SceneContainer1.heroesContainers[0].name);
             //prefab.transform.SetParent(GameObject.Find("HeroesPool1").transform);
            //Debug.Log(prefab.transform.parent.name);
             prefab.transform.SetParent(SceneContainer1.heroesContainers[(int)team].transform);
            // Debug.Log(prefab.transform.parent.name);

        }
    }
}