using System.Collections.Generic;
using UnityEngine;
using System;


namespace TowerHanoi
{
    /// <summary>
    /// Storage disks in slots
    /// </summary>
    public class StockDisks : MonoBehaviour
    {
        //setting
        [SerializeField] int Capacity = 16;
        [SerializeField] float heightOneSlot = 0.1f;

        //private data
        Vector3[] slots;
        List<Disk> disks = new List<Disk>();

        //events
        /// <summary>
        /// Called when the mouse enters the stock
        /// </summary>
        public event Action<StockDisks> On_MouseEnter;
        /// <summary>
        /// Called when the mouse exits the stock
        /// </summary>
        public event Action<StockDisks> On_MouseExit;

        #region MonoBehaviour events
        void Awake()
        {
            slots = new Vector3[Capacity];
            for (int i = 0; i < Capacity; i++)
                slots[i] = transform.position + new Vector3(0, (i + 1) * heightOneSlot, 0);
        }
        void OnMouseEnter()
        {
            if (On_MouseEnter != null)
                On_MouseEnter(this);
        }
        void OnMouseExit()
        {
            if (On_MouseExit != null)
                On_MouseExit(this);
        }
        #endregion

        #region public commands
        /// <summary>
        /// Add disk to the end of the stock
        /// </summary>
        /// <param name="disk"></param>
        public void PutDiskOnTop(Disk disk)
        {
            disk.transform.position = slots[disks.Count];
            disks.Add(disk);
        }
        /// <summary>
        /// Remove last disk from the stock
        /// </summary>
        /// <returns></returns>
        public Disk TakeLastDisk()
        {
            Disk lastDisk = disks[disks.Count - 1];
            disks.RemoveAt(disks.Count - 1);
            return lastDisk;
        }
        /// <summary>
        /// Destroy all disks if any exists
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < disks.Count; i++)
            {
                Destroy(disks[i].gameObject);
            }
            disks.Clear();
        }
        /// <summary>
        /// Return last free position in the stock
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLastFreePos()
        {
            return slots[disks.Count];
        }
        #endregion

    }
}
