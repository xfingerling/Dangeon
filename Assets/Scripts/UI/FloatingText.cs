using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public bool IsActive;

    private Text _text;
    private Vector3 _motion;
    private float _duration;
    private float _lastShow;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        if (IsActive)
            UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        IsActive = true;
        _lastShow = Time.time;

        _text.text = msg;
        _text.fontSize = fontSize;
        _text.color = color;
        transform.position = Camera.main.WorldToScreenPoint(position);
        _motion = motion;
        _duration = duration;

        gameObject.SetActive(IsActive);
    }

    public void Hide()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }

    public void UpdateFloatingText()
    {
        if (!IsActive)
            return;

        if (Time.time - _lastShow > _duration)
            Hide();

        transform.position += _motion * Time.deltaTime;
    }
}
