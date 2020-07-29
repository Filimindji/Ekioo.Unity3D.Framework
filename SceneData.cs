using Assets.Common.Framework;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConceptEvasion.Common.Data
{
    [Serializable]
    public class SceneData
    {
        private SceneData()
        {

        }


        [SerializeField]
        private string _id;

        [SerializeField]
        private string _scenePath;

        [SerializeField]
        private string _name;

        public string Id => _id;


        public string Name => _name;

        public string ScenePath => _scenePath;


        public async Task<bool> LoadAsync()
        {
            if (CanBeLoaded())
            {
                await SceneManager.LoadSceneAsync(_scenePath, LoadSceneMode.Additive).Await();
                return true;
            }
            else
            {
                Debug.Log("Impossible de charger cette scène");
                return false;
            }
        }

        public async Task<bool> UnloadAsync()
        {
            if (CanBeLoaded())
            {
                await SceneManager.UnloadSceneAsync(_scenePath).Await();
                return true;
            }
            else
            {
                Debug.Log("Impossible de décharger cette scène");
                return false;
            }
        }

        public bool CanBeLoaded()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                if (_scenePath == scenePath)
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return _scenePath;
        }


#if UNITY_EDITOR

        public static SceneData Create(UnityEditor.SceneAsset sceneAsset)
        {
            SceneData sceneData = new SceneData();

            if (UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(sceneAsset, out var guid, out long file))
            {
                sceneData._id = guid;
            }
            else
                return null;

            sceneData._name = sceneAsset.name;
            sceneData._scenePath = UnityEditor.AssetDatabase.GetAssetPath(sceneAsset); ;

            return sceneData;
        }
#endif

    }

}
