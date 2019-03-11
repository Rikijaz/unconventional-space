using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuInput : MonoBehaviour {
    [SerializeField] int first_level_scene_ = 1;

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update(); // Call before checking the input

        // Start game
        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("wasdad");
            SceneManager.LoadScene(first_level_scene_);
        }

        // End 
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}

