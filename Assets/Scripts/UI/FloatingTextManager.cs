using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    private GameObject textContainer;
    private GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Start()
    {
        textContainer = gameObject;
        textPrefab = Resources.Load("FloatingText") as GameObject;
    }

    private void Update()
    {
        foreach (FloatingText text in floatingTexts)
        {
            text.UpdateFloatingText();
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.isActive);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform); //TODO при переходе на новую сцену удаляется канвас и контейнер для всплывающего текста
            txt.text = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
