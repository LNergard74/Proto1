using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public bool isLevel1;

    void Update()
    {
        if (gameObjects.Count == 0 && isLevel1)
        {
            SceneManager.LoadScene(2);
        }
        else if(gameObjects.Count == 0 && !isLevel1)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void remove(GameObject Priest)
    {
        gameObjects.Remove(Priest);
    }
}
