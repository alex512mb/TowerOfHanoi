using System;
using System.Collections;
using UnityEngine;

namespace TowerHanoi
{
    /// <summary>
    /// The disk can move to a set destination
    /// </summary>
    public class Disk : MonoBehaviour
    {
        [SerializeField] float speedMove = 1;

        Action OnComplete;
        Vector3 destination;

        float minDistanceToDestination
        {
            get { return speedMove * Time.deltaTime * 2; }
        }

        /// <summary>
        /// Translate the disk to the destination
        /// </summary>
        /// <param name="_destination">the final destination of the disk</param>
        /// <param name="onComplete">will be invoke after finish the work</param>
        /// <param name="height">height where disk will move</param>
        public void StartProgrammReplace(Vector3 _destination, float height, Action onComplete)
        {
            OnComplete = onComplete;
            destination = _destination;
            StartCoroutine(ProgramMoveToDestination(height));
        }
        /// <summary>
        /// Cancel current movement if it is act
        /// </summary>
        public void Cancel()
        {
            StopAllCoroutines();
        }

        //Other methods
        IEnumerator ProgramMoveToDestination(float height)
        {
            //wait for go up
            yield return StartCoroutine(ProgramMoveToPos(new Vector3(transform.position.x, height, transform.position.z)));

            //wait for go to over destination pos
            yield return StartCoroutine(ProgramMoveToPos(new Vector3(destination.x, height, destination.z)));

            //wait for go to destination
            yield return StartCoroutine(ProgramMoveToPos(destination));

            OnComplete();
        }
        IEnumerator ProgramMoveToPos(Vector3 pos)
        {
            float distanceToDestination = Mathf.Infinity;
            while (distanceToDestination > minDistanceToDestination)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, speedMove * Time.deltaTime);
                distanceToDestination = Vector3.Distance(transform.position, pos);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
