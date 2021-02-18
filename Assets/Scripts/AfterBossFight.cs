using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterBossFight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    private void GoNextScene()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SceneManager.LoadScene("Game2");
        }
    }



    // Update is called once per frame
         void Update()
         {
             GoNextScene();
         }
    }
