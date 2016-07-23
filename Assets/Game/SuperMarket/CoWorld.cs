using UnityEngine;
using System.Collections;
using Game.SuperMarket.Service;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

namespace Game.SuperMarket
{
    /// <summary>
    /// 游戏世界
    /// </summary>
    public class CoWorld : MonoBehaviour
    {

        public GoManager GoManager = new GoManager();


        public CoMarket Market;

        public GameObject Canvas;

        public Transform MouseClickPanel;

        // Use this for initialization
        void Start()
        {
            if (Canvas == null)
                Canvas = GameObject.Find("Canvas");
            //if (MouseClickPanel == null)
            //{
            //    //MouseClickPanel = Canvas.transform.FindChild("MouseClickPanel");
            //    var go = GoManager.GetGameObject("MouseClickPanel", "PfbClick", Vector3.down, Canvas.transform);
            //    MouseClickPanel = go.transform;

            //}




        }

        void Update()
        {
            MouseClick();
        }

        void FixedUpdate()
        {

        }


        private void MouseClick()
        {
            // Mouse Click Event
            if (Input.GetMouseButtonDown(0))
            {

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("点击到UI");
                }
                else
                {
                    Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(mRay, out hit))
                    {
                        //Debug.Log(hit.point);
                        //Debug.Log("x:" + hit.point.x + ", z:" + hit.point.z);
                        Debug.Log("Name:" + hit.collider.transform.name);

                        var hcTrans = hit.collider.transform;
                        var furnishComponent = hcTrans.GetComponent<CoFurnish>();
                        if (furnishComponent != null)
                        {
                            var point = Camera.main.WorldToScreenPoint(hit.collider.transform.position);
                            // 显示面板
                            var panel = GoManager.GetGameObject(Consts.PANEL_MOUSE_CLICK, "PfbClick", point, Canvas.transform);
                            MouseClickPanel = panel.transform;

                            return;
                        }
                    }
                    // 关闭UI
                    if (MouseClickPanel != null)
                        GoManager.DestroyObject(MouseClickPanel);
                    Debug.Log("Destroy : ");
                }
            }
        }

    }

}
