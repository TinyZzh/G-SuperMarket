using UnityEngine;
using System.Collections.Generic;
using Okra.Tiled.AStar;

public class Test : MonoBehaviour
{


    public Material Red;
    public Material Green;
    public Material Blue;

	// Use this for initialization
	void Start () {
        int[,] blocks = new int[,]{// 地图数组
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        };

        int[,] ary = new int[,]
        {
            { 1,1},
            { 2,2},
            { 3,3},
        };
        //Array.Reverse(ary);

        Debug.Log("aaa");

        //
        for (int i = 0; i < blocks.GetLength(0); i++)
	    {
	        for (int j = 0; j < blocks.GetLength(1); j++)
	        {
	            if (blocks[i, j] == 0)
	            {
                    var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.position = new Vector3(i, 0, j);
                    MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
	                meshRenderer.material = Blue;
	            }
	        }
        }

        var path = AStarAlgorithm.Find(5, 3, 9, 16, blocks, false);
        //var path = AStarAlgorithm.Find(5, 3, 6, 3, blocks, false);
        Debug.Log("路径：" + path.Count);
	    path.AddFirst(new Point(5, 3));
    }



    public static void ShowPath(LinkedList<Point> path, Material[] materials)
    {
        foreach (var item in path)
        {
            //GameObject go = (GameObject)Instantiate(null, new Vector3(item.X, 0, item.Y), Quaternion.identity);

            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3(item.X, 0, item.Y);
            var meshRenderer = obj.GetComponent<MeshRenderer>();

            meshRenderer.material = materials[3];

            //if (path.First.Value.Equals(item))
            //{
            //    meshRenderer.material = materials[0];
            //    obj.name = "Start";
            //}
            //else if (path.Last.Value.Equals(item))
            //{
            //    meshRenderer.material = materials[1];
            //    obj.name = "End";
            //}
            //else
            //{
            //    meshRenderer.material = materials[3];
            //}
        }
    }

    public static void ShowNode(Point point, string name, Material material)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = new Vector3(point.X, 0, point.Y);
        obj.transform.name = name;
        var meshRenderer = obj.GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
