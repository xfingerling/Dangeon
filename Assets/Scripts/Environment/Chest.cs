using UnityEngine;

public class Chest : Collectabe
{
    [SerializeField] private Sprite emptyChest;
    [SerializeField] private int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;

            GameManager.instance.pesos += pesosAmount;

            string msg = $"+{pesosAmount} pesos!";
            GameManager.instance.ShowText(msg, 15, Color.yellow, transform.position, Vector3.up * 50, 1.5f);
        }
    }
}
