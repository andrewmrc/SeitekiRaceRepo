using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPathScript : MonoBehaviour
{
    public Color rayColor = Color.white;
    public List<Transform> pathList = new List<Transform>();
    Transform[] pathArray;

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        pathArray = GetComponentsInChildren<Transform>();
        pathList.Clear();

        foreach (var path in pathArray)
        {
            if (path != this.transform)
            {
                pathList.Add(path);
            }
        }

        for (int i = 0; i < pathList.Count; i++)
        {
            Vector3 position = pathList[i].position;
            if (i > 0)
            {
                Vector3 previous = pathList[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, .3f);
            }
        }
    }



}
