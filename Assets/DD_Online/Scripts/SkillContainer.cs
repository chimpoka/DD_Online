﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;

//using System.IO;
//using Newtonsoft.Json;


public class SkillContainer
{
    private static SkillContainer _instance;
    public static SkillContainer Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SkillContainer();

            return _instance;  
        }
    }

    private JSONNode parsedJson;
    private string fileName = "Assets/DD_Online/Scripts/Skills.txt";



    public void init()
    {
        parsedJson = JSON.Parse(File.ReadAllText(fileName));
    }

    public Skill getSkill(HeroClass heroClass, SkillName skillName)
    {
        var heroField = parsedJson[heroClass.ToString()];
        if (heroField.Count == 0) Debug.LogError("No hero '" + heroClass + "'! Check '" + fileName + "'");

        var skillJson = parsedJson[heroClass.ToString()][skillName.ToString()];
        if (skillJson.Count == 0) Debug.LogError("Hero '" + heroClass + "' has no skill: '" + skillName + "'! Check '" + fileName + "'");

        Skill skill = parseJson(skillJson, skillName, heroClass);

        return skill;
    }

 

    private Skill parseJson(JSONNode json, SkillName skillName, HeroClass heroClass)
    {
        bool multipleTarget;
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

            if (effectTable.Value["SelfTargeted"] == "True")
                effect.selfTargeted = true;
            else
                effect.selfTargeted = false;


            effect.duration = effectTable.Value["Duration"];
            effect.attribute = effectTable.Value["Attribute"];
            effects.Add(effect);
        }


        if (json["MultipleTarget"] == "True")
            multipleTarget = true;
        else
            multipleTarget = false;

        // targetTeam
        foreach (TargetTeam _targetTeam in (TargetTeam[])Enum.GetValues(typeof(TargetTeam)))
            if (_targetTeam.ToString() == json["Team"]) targetTeam = _targetTeam;
        

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
            if (s[i] == '1')
                usePositions.Add(i + 1);
        }

        // value
        value = json["Value"];



        Skill skill = new Skill
        {
            skillName = skillName,
            multipleTarget = multipleTarget,
            targetTeam = targetTeam,
            targetPositions = targetPositions,
            usePositions = usePositions,
            effects = effects
        };

        return skill;
    }
}
