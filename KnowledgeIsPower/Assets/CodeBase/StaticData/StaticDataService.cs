using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(element => element.MonsterTypeId);
        }

        public MonsterStaticData GetMonsterById(MonsterTypeId id)
        {
            if (_monsters.TryGetValue(id, out MonsterStaticData monsterStaticData))
            {
                return monsterStaticData;
            }

            throw new ArgumentException($"Don't have this object of this type {id}");
        }
    }
}