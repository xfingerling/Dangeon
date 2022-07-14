using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextInteractor : Interactor
{
    private GameObject _textContainer;
    private Canvas _canvas;
    private GameObject _floatingTextPrefab;
    private List<FloatingText> _floatingTexts = new List<FloatingText>();

    public override void Initialize()
    {
        //Create Canvas
        _textContainer = new GameObject("[FloatTextCanvas]");
        _textContainer.AddComponent<Canvas>();
        _textContainer.AddComponent<CanvasScaler>();
        _textContainer.AddComponent<GraphicRaycaster>();

        _canvas = _textContainer.GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        _floatingTextPrefab = Resources.Load("UI/FloatingText") as GameObject;

        //Create pool
        for (int i = 0; i < 5; i++)
        {
            FloatingText text = CreateFloatingText();
            _floatingTexts.Add(text);
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.Show(msg, fontSize, color, position, motion, duration);
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = _floatingTexts.Find(t => !t.IsActive);

        if (txt == null)
        {
            txt = CreateFloatingText();
            _floatingTexts.Add(txt);
        }

        return txt;
    }

    private FloatingText CreateFloatingText()
    {
        GameObject floatingText = Object.Instantiate(_floatingTextPrefab);
        floatingText.transform.SetParent(_textContainer.transform);
        floatingText.SetActive(false);


        return floatingText.GetComponent<FloatingText>();
    }
}
