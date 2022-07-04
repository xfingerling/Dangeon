using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool isActive;
    public GameObject go;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShow;

    public void Show()
    {
        isActive = true;
        lastShow = Time.time;
        go.SetActive(isActive);
    }

    public void Hide()
    {
        isActive = false;
        go.SetActive(isActive);
    }

    public void UpdateFloatingText()
    {
        if (!isActive)
            return;

        if (Time.time - lastShow > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
