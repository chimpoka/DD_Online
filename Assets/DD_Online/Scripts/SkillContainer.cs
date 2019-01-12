using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;

//using System.IO;
//using Newtonsoft.Json;


public class SkillContainer
{
    

    public static Skill getSkill(HeroClass heroClass, SkillName skillName)
    {
        //TODO: save file once and then read it
        string SkillsJson = "Assets/DD_Online/Scripts/Skills.txt";
        var file = JSON.Parse(File.ReadAllText(SkillsJson));
        Skill skill = parseJson(file[heroClass.ToString()][skillName.ToString()], skillName, heroClass);

        return skill;
    }

 

    private static Skill parseJson(JSONNode json, SkillName skillName, HeroClass heroClass)
    {
        SkillType type = SkillType.Default;
        TargetTeam targetTeam = TargetTeam.Default;
        List<int> targetPositions;
        List<int> usePositions;
        int value;
        List<Effect> effects = new List<Effect>();



        // effects
        foreach (var effectTable in json["Effects"])
        {
            Effect effect = new Effect();

            foreach (SkillType _type in (SkillType[])Enum.GetValues(typeof(SkillType)))
                if (_type.ToString() == effectTable.Value["Type"]) effect.type = _type;
            
            effect.value = effectTable.Value["Value"];

            if (effectTable.Value["Target"] == "True")
                effect.target = true;
            else
                effect.target = false;


            effect.duration = effectTable.Value["Duration"];
            effect.attribute = effectTable.Value["Attribute"];
            effects.Add(effect);
            //Debug.Log(effect.type.ToString());
            //Debug.Log(effect.value.ToString());
            //Debug.Log(effect.target.ToString());
            //Debug.Log(effect.duration.ToString());
            //Debug.Log(effect.attribute.ToString());
        }

        //// type
        //foreach (SkillType _type in (SkillType[])Enum.GetValues(typeof(SkillType)))
        //{
        //    if (_type.ToString() == json["Type"]) type = _type;
        //}

        // targetTeam
        foreach (TargetTeam _targetTeam in (TargetTeam[])Enum.GetValues(typeof(TargetTeam)))
        {
            if (_targetTeam.ToString() == json["Team"]) targetTeam = _targetTeam;
        }

        //targetPositions
        string s = json["TargetPositions"];
        targetPositions = new List<int>();
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '1')
                targetPositions.Add(i + 1);
        }

        //usePositions
        s = json["UsePositions"];
        usePositions = new List<int>();
        for (int i = 0; i < s.Length; ++i)
        {
            if (heroClass == HeroClass.Priest)
                Debug.Log("Skill: " + skillName + ";  s[i]: " + s[i]);

            if (s[i] == '1')
            {
                //Debug.Log("Skill: " + skillName + ";  usePos: " + i);
                usePositions.Add(i + 1);
                //if (heroClass == HeroClass.Priest)
                //{
                //    int j = i + 1;
                //    Debug.Log("Skill: " + skillName + ";  usePos: " + j);
                //}
            }
        }

        // value
        value = json["Value"];



        Skill skill = new Skill
        {
            skillName = skillName,
            type = type,
            targetTeam = targetTeam,
            targetPositions = targetPositions,
            usePositions = usePositions,
            value = value
        };

        return skill;
    }
}
