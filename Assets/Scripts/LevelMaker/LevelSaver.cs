using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LevelMaker
{
    public class LevelSaver : MonoBehaviour
    {

        public static void SaveLevel(string file)
        {
            using (Stream stream = File.Open(Application.persistentDataPath + "/" + file + ".lvl", FileMode.Create))
            {
                
            }
                
        }
    }
}
