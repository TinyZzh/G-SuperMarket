using System.Linq;
using UnityEngine;
using Game.SuperMarket.Impl;
using Game.SuperMarket.Service;
using Okra.Tiled;
using Okra.Tiled.AStar;
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

        #region  UI面板

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
        /// <summary>
        /// 鼠标上的Go
        /// </summary>
        public GameObject GoOnMouse;

        public GameObject Canvas;

        public Transform MouseClickPanel;
        #endregion

        // Use this for initialization
        void Start()
        {
            if (Canvas == null)
                Canvas = GameObject.Find("Canvas");
            Market = transform.GetComponent<CoMarket>();


        }

        void Update()
        {
            OnMouseClick();

            OnMouseMove();
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
                #region 摆放事件
                var mousePosition = Input.mousePosition;
                if (TempGoFurnish != null)
                {
                    Market.Layout[TemPoint.X, TemPoint.Y] = new Grid(TemPoint, GridType.Shelf, 10);

                    TempGoFurnish.transform.position = new Vector3(TemPoint.X, 0, TemPoint.Y);
                    TempGoFurnish.transform.name = "Name";
                    var meshRenderer = TempGoFurnish.GetComponent<MeshRenderer>();
                    meshRenderer.material = Market.Blue;

                    TempGoFurnish = null;
                    return;
                }
                #endregion

                #region 鼠标点击货架事件
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("点击到UI");
                }
                else
                {
                    Ray mRay = Camera.main.ScreenPointToRay(mousePosition);
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
                    {
                        GoManager.DestroyObject(MouseClickPanel);
                        Debug.Log("Destroy : ");
                    }
                    // 

                }
                #endregion
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        public void OnMouseMove()
        {
            if (TempGoFurnish != null)
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(mRay, out hit))
                {
                    var point = new Point((int)hit.point.x, (int)hit.point.z);
                    if (!TemPoint.Equals(point))
                    {
                        TempGoFurnish.transform.position = new Vector3(point.X, 0, point.Y);
                        TemPoint = point;
                        Debug.Log("[" + point.X + "," + point.Y + "]");
                    }
                }
            }
        }

        public GameObject TempGoFurnish;

        public Point TemPoint = Point.Zero;


        public void OnPrePutFurnish()
        {
            if (TempGoFurnish != null) return;
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            TempGoFurnish = GoManager.CreateCube(point, "X", Market.Green, new[] {typeof (CoFurnish)});
            var boxColloder = TempGoFurnish.GetComponent<BoxCollider>();
            boxColloder.enabled = false;
        }

        public void OnPutFurnish()
        {
            if (TempGoFurnish == null)
            {

            }
        }

        public void OnWelcome()
        {
            var path = Market.LookupPath();

            var mUnitGo = Test.ShowNode(Market.GatePoint, "coUnit", Market.Red);
            var coUnit = mUnitGo.AddComponent<CoUnit>();
            coUnit.Path = path.ToArray();
            coUnit.Index = 0;
            coUnit.Unit = mUnitGo.transform;
        }


    }
}
