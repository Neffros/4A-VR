using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PGSauce.Core.Utilities
{
    public abstract class SOSingleton<T> : SOSingletonBase where T : SOSingletonBase
    {
        static T _instance = null;
        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                return _instance;
            }
        }
    }
}
