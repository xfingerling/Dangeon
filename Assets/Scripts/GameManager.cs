using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(characterMenu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    //Resourse
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject hud;
    public Animator deathMenuAnim;
    public CharacterMenu characterMenu;

    //Logic
    public int preferredSkin;
    public int pesos;
    public int expirience;


    //Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //Is the weapon max level?
        if (weaponPrices.Count <= weapon.WeaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.WeaponLevel])
        {
            pesos -= weaponPrices[weapon.WeaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Hitpoint bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //Expirience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (expirience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //Max level
                return r;
        }

        return r;
    }
    public int GetXpToLevel(int lvl)
    {
        int r = 0;
        int xp = 0;

        while (r < lvl)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        expirience += xp;

        if (currentLevel < GetCurrentLevel())
            OnLevelUp();

    }
    public void OnLevelUp()
    {
        Debug.Log("Level Up");
        player.OnLevelUp();
        OnHitpointChange();
    }

    //On scene loaded 
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        //Player spawn position
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Death menu and Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        SceneManager.LoadScene("Main");
        player.Respawn();
    }

    //Save State
    /*
     * INT preferredSkin
     * INT pesos
     * INT expirience
     * INT weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += preferredSkin.ToString() + "|";
        s += pesos.ToString() + "|";
        s += expirience.ToString() + "|";
        s += weapon.WeaponLevel.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Save State");
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        //.....

        //Pesos
        pesos = int.Parse(data[1]);

        //Expirience
        expirience = int.Parse(data[2]);

        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        Debug.Log("Load State");
    }

}
