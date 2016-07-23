using System.Collections.Generic;
using UnityEngine;

namespace Game.SuperMarket.Service
{
    /// <summary>
    /// 简单的资源管理工具
    /// </summary>
    public class GoManager
    {

        private Dictionary<string, GameObject> _resources = new Dictionary<string, GameObject>();

        public GameObject GetGameObject(string objName, string resName, Vector3 position, Transform parent)
        {
            GameObject go = null;
            if (_resources.ContainsKey(objName))
            {
                go = _resources[objName];
            }
            else
            {
                go = (GameObject)Object.Instantiate(Resources.Load(resName));
                if (go == null) return null;
                go.transform.name = objName;
                _resources.Add(objName, go);
            }
            go.transform.position = position;
            go.transform.SetParent(parent);
            go.SetActive(true);
            return go;
        }

        public void DestroyObject(GameObject go)
        {
            go.SetActive(false);
        }

        public void DestroyObject(Transform tf)
        {
            DestroyObject(tf.gameObject);
        }



    }
}