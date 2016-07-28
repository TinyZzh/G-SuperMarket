using System;
using System.Collections.Generic;
using Game.SuperMarket.Component;
using Okra.Tiled.AStar;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.SuperMarket.Service
{
    /// <summary>
    /// 简单的资源管理工具
    /// </summary>
    public class GoManager
    {

        #region 简单的对象池
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
        #endregion


        public static GameObject UpdateCube(GameObject go, Vector3 point, string name, Material material, Type[] components)
        {

            return go;
        }

        /// <summary>
        /// 创建新方块
        /// </summary>
        /// <param name="point"></param>
        /// <param name="name"></param>
        /// <param name="material"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static GameObject CreateCube(Vector3 point, string name, Material material, Type[] components)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            foreach (var component in components)
            {
                obj.AddComponent(component);
            }
            obj.transform.position = point;
            obj.transform.name = name;
            var meshRenderer = obj.GetComponent<MeshRenderer>();
            meshRenderer.material = material;
            return obj;
        }

    }
}