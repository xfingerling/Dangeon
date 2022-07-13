using UnityEngine;

public class SpawnPositionInteractor : Interactor
{

    public Transform SpawpPosition;

    public override void Initialize()
    {
        base.Initialize();

        SpawpPosition = GameObject.FindGameObjectWithTag("SpawnPosition").transform;
    }
}
