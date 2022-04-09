using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    
    void Awake()
    {
        // Singleton
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersist > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
    }

    public void ResetScenePersist()
    {
        Destroy(this.gameObject);
    }

   
}
