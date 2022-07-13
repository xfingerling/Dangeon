using UnityEngine;

public class CameraInteractor : Interactor
{
    public Camera MainCamera { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        MainCamera = Camera.main;
        MainCamera.gameObject.AddComponent<CameraControl>();
    }
}
