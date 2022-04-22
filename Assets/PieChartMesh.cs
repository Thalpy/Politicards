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

    public float pieDepth = 0.25f;

   void Start()
    {
        Mesh mesh = new Mesh();

        mf = GetComponent<MeshFilter>();
        
        mr = GetComponent<MeshRenderer>();

        topFacePoints = new List<Vector3>();

        bottomFacePoints = new List<Vector3>();

        topFaceTriangles = new List<int>();

        //Generate the circles

        Vector3 origin = new Vector3(0, 0, 0);

        int numberOfDegrees = (int)(proportion * 360); // lazily rounding, I'm sure this won't cause bugs.......

        Debug.Log(numberOfDegrees);

        topFacePoints.Add(origin); 

        bottomFacePoints.Add(new Vector3(0, 0, pieDepth));

        //create the points....

        for(int i = 0; i < numberOfDegrees; i++)
        {
            
            var x = this.pieRadius * Mathf.Cos(i * Mathf.Deg2Rad);
            var y = this.pieRadius * Mathf.Sin(i * Mathf.Deg2Rad);
            var z = 0;
            Vector3 point = new Vector3(x, y, z);

            var x2 = this.pieRadius * Mathf.Cos(i * Mathf.Deg2Rad);
            var y2 = this.pieRadius * Mathf.Sin(i * Mathf.Deg2Rad);
            var z2 = pieDepth;
            Vector3 point2 = new Vector3(x2, y2, z2);

            Debug.Log($"Adding a point to the vertex list: ({x}, {y}, {z}), currently  on iteration {i}");


            topFacePoints.Add(point);   
            bottomFacePoints.Add(point2);

        }

        Debug.Log($"There are {topFacePoints.Count} points in the top face");

        //create facial triangles....

        for(int i = 0; i < (topFacePoints.Count -2); i++)
        {
            Debug.Log($"Creating new triangle with vertices: {0}, {i+1}, {i+2}");
            topFaceTriangles.Add(i+2);
            topFaceTriangles.Add(i+1);            
            topFaceTriangles.Add(0);

            topFaceTriangles.Add(i+2+topFacePoints.Count);
            topFaceTriangles.Add(topFacePoints.Count);
            topFaceTriangles.Add(i+1+topFacePoints.Count);
            
        }

        //create curved edge triangles

        for(int i = 0; i < (topFacePoints.Count -2); i++)
        {
            topFaceTriangles.Add(i);
            topFaceTriangles.Add(i+1);
            topFaceTriangles.Add(i + topFacePoints.Count + 1);
            topFaceTriangles.Add(i + topFacePoints.Count + 1);
            topFaceTriangles.Add(i+1);
            topFaceTriangles.Add(i + topFacePoints.Count + 2);

        }

        //Add the long straight edges

        topFaceTriangles.Add(topFacePoints.Count+1);
        topFaceTriangles.Add(1);
        topFaceTriangles.Add(0);
        
        topFaceTriangles.Add(topFacePoints.Count);
        topFaceTriangles.Add(topFacePoints.Count+1);
        topFaceTriangles.Add(0);
        
        
        
        
        

        topFaceTriangles.Add((2*topFacePoints.Count)-1);
        topFaceTriangles.Add(0);
        topFaceTriangles.Add(topFacePoints.Count);
        

        topFaceTriangles.Add((2*topFacePoints.Count)-1);
        topFaceTriangles.Add(topFacePoints.Count-1);
        topFaceTriangles.Add(0);
        


        List<Vector3> vertexList = new List<Vector3>();
        vertexList.AddRange(topFacePoints);
        vertexList.AddRange(bottomFacePoints);
        //mesh.vertices = topFacePoints.ToArray();
        mesh.vertices = vertexList.ToArray();
        mesh.triangles = topFaceTriangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
        mf.mesh = mesh;
        foreach(int point in topFaceTriangles)
        {
            Debug.Log(point);
        }
        StartCoroutine("DrawPoints", vertexList);
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
