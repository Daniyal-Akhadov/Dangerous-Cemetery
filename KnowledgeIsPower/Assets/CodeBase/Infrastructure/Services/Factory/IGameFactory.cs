using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject Hero { get; }
        event Action HeroCreated;
        void Cleanup();
    }
}