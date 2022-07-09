using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneManagerBase
{
    public event Action<Scene> OnSceneLoadedEvent;
    public Scene Scene { get; private set; }

    public bool IsLoading { get; private set; }

    protected Dictionary<string, SceneConfig> sceneConfigMap;

    public SceneManagerBase()
    {
        sceneConfigMap = new Dictionary<string, SceneConfig>();
    }

    public abstract void InitScenesMap();

    public T GetRepository<T>() where T : Repository
    {
        return Scene.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return Scene.GetInteractor<T>();
    }

    public Coroutine LoadCurrentSceneAsync()
    {
        if (IsLoading)
            throw new Exception("Sceen is loading now");

        var sceneName = SceneManager.GetActiveScene().name;
        var config = sceneConfigMap[sceneName];
        return Coroutines.StartRoutine(LoadCurrentSceneRoutine(config));
    }

    private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig)
    {
        IsLoading = true;

        yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));

        IsLoading = false;
        OnSceneLoadedEvent?.Invoke(Scene);
    }

    public Coroutine LoadNewSceneAsync(string sceneName)
    {
        if (IsLoading)
            throw new Exception("Sceen is loading now");

        var config = sceneConfigMap[sceneName];
        return Coroutines.StartRoutine(LoadNewSceneRoutine(config));
    }

    private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
    {
        IsLoading = true;

        yield return Coroutines.StartRoutine(LoadSceneRoutine(sceneConfig));
        yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));

        IsLoading = false;
        OnSceneLoadedEvent?.Invoke(Scene);
    }

    private IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
    {
        var async = SceneManager.LoadSceneAsync(sceneConfig.sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            yield return null;

        async.allowSceneActivation = true;
    }

    private IEnumerator InitializeSceneRoutine(SceneConfig sceneConfig)
    {
        Scene = new Scene(sceneConfig);

        yield return Scene.InitializeAsync();
    }
}
