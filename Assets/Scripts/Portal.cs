using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    [SerializeField] private string[] _sceneNames;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            //Teleport the player
            GameManager.instance.SaveState();
            string sceneName = _sceneNames[Random.Range(0, _sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
        }
    }
}
