using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class PieChartMesh : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    float pieRadius = 1.0f; // radius of our pie chart

    double proportion = 0.25; // proportion of available political power assigned to the entity so represented

    public MeshFilter mf;

    public MeshRenderer mr;

    List<Vector3> topFacePoints;

    List<Vector3> bottomFacePoints;

    List<int> topFaceTriangles;

    List<int> bottomFaceTriangles;

    public GameObject PointMarker;

    List<int> edgeTriangles; //Haven't considered the long edge in here.....

   void Start()
    {
        Mesh mesh = new Mesh();

        mf = GetComponent<MeshFilter>();
        
        mr = GetComponent<MeshRenderer>();

        topFacePoints = new List<Vector3>();

        topFaceTriangles = new List<int>();

        //Generate the circles

        Vector3 origin = new Vector3(0, 0, 0);

        int numberOfDegrees = (int)(proportion * 360); // lazily rounding, I'm sure this won't cause bugs.......

        Debug.Log(numberOfDegrees);

        topFacePoints.Add(origin); 

        //create the points....

        for(int i = 0; i < numberOfDegrees; i++)
        {
            
            var x = this.pieRadius * Mathf.Cos(i * Mathf.Deg2Rad);
            var y = this.pieRadius * Mathf.Sin(i * Mathf.Deg2Rad);
            var z = 0;
            Vector3 point = new Vector3(x, y, z);

            Debug.Log($"Adding a point to the vertex list: ({x}, {y}, {z}), currently  on iteration {i}");


            topFacePoints.Add(point);   

        }

        Debug.Log($"There are {topFacePoints.Count} points in the top face");

        //create top face triangles.....

        for(int i = 0; i < (topFacePoints.Count -2); i++)
        {
            Debug.Log($"Creating new triangle with vertices: {0}, {i+1}, {i+2}");
            topFaceTriangles.Add(i+2);
            topFaceTriangles.Add(i+1);            
            topFaceTriangles.Add(0);
        }

        mesh.vertices = topFacePoints.ToArray();
        mesh.triangles = topFaceTriangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
        mf.mesh = mesh;
        foreach(int point in topFaceTriangles)
        {
            Debug.Log(point);
        }
        //StartCoroutine("DrawPoints", topFacePoints);
        //StartCoroutine("DrawTriangles", topFaceTriangles);
        
    }


#region debug drawing functions
    IEnumerator DrawPoints(List<Vector3> points)
    {
        foreach(Vector3 point in points)
        {
            Instantiate(PointMarker, point, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator DrawTriangles(List<int> indicies)
    {
        foreach (int num in indicies)
        {
            Gizmos.DrawLine(topFacePoints[num], topFacePoints[num+1]);
            yield return new WaitForSeconds(0.1f);
        }
    }

#endregion

}
