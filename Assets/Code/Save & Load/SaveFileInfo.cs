using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Database
{
    [CreateAssetMenu(fileName ="SaveFileInfo", menuName = "Save file info", order = 0)]
    public class SaveFileInfo : ScriptableObject
    {
        private const string FILE_NAME = "savdat.dat";

        public string FilePath
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, FILE_NAME);
            }
        }
    }
}
