using UnityEngine;

public class UIInterface : MonoBehaviour
{
    [SerializeField] private GameObject _uiLayerHUD;
    [SerializeField] private GameObject _uiLayerFXUnderPopup;
    [SerializeField] private GameObject _uiLayerPopup;
    [SerializeField] private GameObject _uiLayerFXOverPopup;
    [SerializeField] private GameObject _uiLayerLoadingScreen;

    public GameObject UILayerHUD => _uiLayerHUD;
    public GameObject UILayerFXUnderPopup => _uiLayerFXUnderPopup;
    public GameObject UILayerPopup => _uiLayerPopup;
    public GameObject UILayerFXOverPopup => _uiLayerFXOverPopup;
    public GameObject UILayerLoadingScreen => _uiLayerLoadingScreen;
}
