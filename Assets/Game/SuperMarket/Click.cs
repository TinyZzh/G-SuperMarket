using UnityEngine;
using System.Collections;
using Okra.Tiled;

namespace Game.SuperMarket
{
    /// <summary>
    /// 处理游戏中的鼠标点击事件
    /// </summary>
    public class Click : MonoBehaviour
    {

        public GameObject BtnGo;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(mRay, out hit))
                {
                    Debug.Log(hit.point);
                    Debug.Log("x:" + hit.point.x + ", z:" + hit.point.z);

                    Debug.Log("Name:" + hit.collider.transform.name);

                    var hcTrans = hit.collider.transform;

                    // 显示面板
                    var point = Camera.main.WorldToScreenPoint(hit.collider.transform.position);
                    var rectTransform = BtnGo.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        rectTransform.position = point;

                    }

                    var shelf = hcTrans.GetComponent<Shelf>();
                    if (shelf != null)
                    {
                        // Vector2 screenpos = Camera.main.WorldToScreenPoint(transform.position);
                    }


                }
            }

        }
    }

}