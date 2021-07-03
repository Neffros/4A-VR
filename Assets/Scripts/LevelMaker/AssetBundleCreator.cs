using System.IO;
using UnityEditor;
using UnityEngine;

namespace LevelMaker
{
    public class AssetBundleCreator
    {
        public static string assetBundleDirectory = "Assets/AssetBundles/";

        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAllAssetBundles()
        {

            //if main directory doesnt exist create it
            if (Directory.Exists(assetBundleDirectory))
            {
                Directory.Delete(assetBundleDirectory, true);
            }

            Directory.CreateDirectory(assetBundleDirectory);

            //create bundles for all platform (use IOS for editor support on MAC but must be on IOS build platform)
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AppendPlatformToFileName("Windows");
            Debug.Log("Windows bundle created...");

            //BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);
            //AppendPlatformToFileName("Linux");
            //Debug.Log("Linux bundle created...");

            RemoveSpacesInFileNames();

            AssetDatabase.Refresh();
            Debug.Log("Process complete!");
        }

        static void RemoveSpacesInFileNames()
        {
            foreach (string path in Directory.GetFiles(assetBundleDirectory))
            {
                string oldName = path;
                string newName = path.Replace(' ', '-');
                File.Move(oldName, newName);
            }
        }


        static void AppendPlatformToFileName(string platform)
        {
            foreach (string path in Directory.GetFiles(assetBundleDirectory))
            {
                //get filename
                string[] files = path.Split('/');
                string fileName = files[files.Length - 1];

                //delete files we dont need
                if (fileName.Contains(".") || fileName.Contains("Bundle"))
                {
                    File.Delete(path);
                }
                else if (!fileName.Contains("-"))
                {
                    //append platform to filename
                    FileInfo info = new FileInfo(path);
                    info.MoveTo(path + "-" + platform);
                }
            }

        }
    }
}