using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public bool isLevel1;

    //checks is all the preists have died in the scene and starts the next level or go to the victory screen
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

    //allows accsess to other scripts to remove objects from the list
    public void remove(GameObject Priest)
    {
        gameObjects.Remove(Priest);
    }
}
