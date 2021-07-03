using System.IO;
using UnityEngine;

namespace LevelMaker
{
    public class LevelLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            LoadObject("Cube");
        }

        void LoadObject(string objName)
        {
            
           
            //var loadedAsset = AssetBundle.LoadFromFile("E:\\Unity\\4A-VR\\4A-VR\\Assets\\Prefabs\\LevelMaker\\" + objName + ".prefab");
            var loadedAsset = AssetBundle.LoadFromFile( Path.Combine(Application.streamingAssetsPath, "Cube"));

            if (loadedAsset == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }
            var prefab = loadedAsset.LoadAsset<GameObject>("MyObject");
            Instantiate(prefab);
            loadedAsset.Unload(false);
        }
    }
}
