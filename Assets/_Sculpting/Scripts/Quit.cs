using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class Quit : MonoBehaviour
    {
        public void ExitApp(){
            //Application.Quit ();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
        
        public void LoadLobby()
        {
            SceneManager.LoadScene("LevelLobby");
        }
        
        public void LoadSculptScene()
        {
            SceneManager.LoadScene("Sculpting");
        }
    }
}
