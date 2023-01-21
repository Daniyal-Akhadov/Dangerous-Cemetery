using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterStaticData", menuName = "StaticData/MonsterStaticData")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        [Range(1f, 100f)] public float Hp = 20f;

        [Range(1f, 30f)]
        public float MoveSpeed = 3f;

        [Range(1f, 30f)]
        public float Damage = 5f;

        [Range(1f, 10f)]
        public float Cooldown = 2f;

        [Range(0.5f, 1f)]
        public float RadiusAttack = 0.6f;

        [Range(0.5f, 1f)]
        public float EffectiveDistance = 0.6f;

        public GameObject Prefab;
    }
}