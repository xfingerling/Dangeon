using System;
using System.Collections.Generic;

public class SceneConfigLevel1 : SceneConfig
{
    public const string SCENE_NAME = "Level1";
    public override string sceneName => SCENE_NAME;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        CreateInteractor<BankInteractor>(interactorsMap);
        CreateInteractor<PlayerInteractor>(interactorsMap);
        CreateInteractor<CameraInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        CreateRepository<BankRepository>(repositoriesMap);

        return repositoriesMap;
    }
}
