using UnityEngine;
using Game.SuperMarket.Impl;
using Game.SuperMarket.Service;
using UnityEngine.EventSystems;

namespace Game.SuperMarket.Component
{
    /// <summary>
    /// 游戏世界
    /// </summary>
    public class CoWorld : MonoBehaviour
    {

        public static GoManager GoManager = new GoManager();


        public CoMarket Market;

        public GameObject Canvas;

        public Transform MouseClickPanel;


        /// <summary>
        /// 左上角角色信息
        /// </summary>
        public GameObject RolePanel;
        /// <summary>
        /// 工具面板 - 仓库.迎客
        /// </summary>
        public GameObject ToolPanel;
        /// <summary>
        /// 活动小图标
        /// </summary>
        public GameObject ActivityPanel;




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
            OnMouseClick();
        }

        void FixedUpdate()
        {

        }

        /// <summary>
        /// 处理鼠标点击
        /// </summary>
        private void OnMouseClick()
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
