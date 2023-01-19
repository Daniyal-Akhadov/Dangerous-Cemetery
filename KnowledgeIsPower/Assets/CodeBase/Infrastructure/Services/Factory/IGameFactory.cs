using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
    }
}