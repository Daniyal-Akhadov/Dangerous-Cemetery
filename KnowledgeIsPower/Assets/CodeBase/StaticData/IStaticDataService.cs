using CodeBase.Infrastructure.Services;
using CodeBase.Logic;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData GetMonsterById(MonsterTypeId id);
    }
}