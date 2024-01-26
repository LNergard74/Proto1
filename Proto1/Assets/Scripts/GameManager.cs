using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();

    void Update()
    {
        if (gameObjects.Count == 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void remove(GameObject Priest)
    {
        gameObjects.Remove(Priest);
    }
}
