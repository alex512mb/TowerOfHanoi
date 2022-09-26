using UnityEngine;

namespace TowerHanoi
{
    /// <summary>
    ///  Let a user override role of stocks
    /// </summary>
    public class UISetStockRoles : MonoBehaviour
    {
        [SerializeField] Transform markStockA;
        [SerializeField] Transform markStockB;

        [SerializeField] StockDisks[] stocks;
        [SerializeField] DisksManager manager;

        StockDisks selectedStockA;
        StockDisks stockUnderMouse;

        //MonoBehaviour events
        void Start()
        {
            markStockA.gameObject.SetActive(false);
            markStockB.gameObject.SetActive(false);
        }
        void OnEnable()
        {
            for (int i = 0; i < stocks.Length; i++)
            {
                stocks[i].On_MouseEnter += MouseEnterOverStock;
                stocks[i].On_MouseExit += MouseExitOverStock;
            }
        }
        void OnDisable()
        {
            for (int i = 0; i < stocks.Length; i++)
            {
                stocks[i].On_MouseEnter -= MouseEnterOverStock;
                stocks[i].On_MouseExit -= MouseExitOverStock;
            }
        }
        void LateUpdate()
        {
            //select stock A
            if (Input.GetMouseButtonDown(0) && stockUnderMouse != null)
            {
                selectedStockA = stockUnderMouse;

                markStockA.gameObject.SetActive(true);
                markStockA.parent = selectedStockA.transform;
                markStockA.localPosition = Vector3.zero;
            }
            if (Input.GetMouseButtonUp(0))
            {
                //deselect all
                if (stockUnderMouse == null)
                {
                    selectedStockA = null;
                    markStockB.gameObject.SetActive(false);
                    markStockA.gameObject.SetActive(false);
                }
                //apply a new setup
                else
                {
                    manager.OverrideStocksDestination(selectedStockA, stockUnderMouse, GetStock_C(selectedStockA, stockUnderMouse));

                    //reset all
                    selectedStockA = null;
                    markStockA.gameObject.SetActive(false);
                    markStockB.gameObject.SetActive(false);
                }

            }
        }

        //event handlers
        void MouseEnterOverStock(StockDisks stock)
        {
            stockUnderMouse = stock;
            if (selectedStockA != null && selectedStockA != stock)
            {
                markStockB.gameObject.SetActive(true);
                markStockB.parent = stock.transform;
                markStockB.localPosition = Vector3.zero;
            }
        }
        void MouseExitOverStock(StockDisks stock)
        {
            stockUnderMouse = null;
            if (selectedStockA != null)
                markStockB.gameObject.SetActive(false);
        }

        //other
        StockDisks GetStock_C(StockDisks a, StockDisks b)
        {
            for (int i = 0; i < stocks.Length; i++)
            {
                if (stocks[i] != a && stocks[i] != b)
                    return stocks[i];
            }
            return stocks[0];
        }
    }
}
