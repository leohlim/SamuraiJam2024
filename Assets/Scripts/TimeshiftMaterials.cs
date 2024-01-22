using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeshiftMaterials : MonoBehaviour
{
    // Start is called before the first frame update
    public Shader timeshiftShader;

    private Material[] materials;

    public GameObject monitorheadPrefab;

    public float updateInterval = 0.1f;

    private float lastUpdateTime;
    void Start()
    {
        materials = FindMaterialsWithShader(timeshiftShader);

        UpdateShaderPositions();
    }

    private void Update()
    {
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            UpdateShaderPositions();
            lastUpdateTime = Time.time;
        }
    }
    private void UpdateShaderPositions()
    {
        Vector3 enemyPosition = monitorheadPrefab.transform.position;

        foreach(Material mat in materials)
        {
            mat.SetVector("_LightPos", new Vector4(enemyPosition.x, enemyPosition.y, enemyPosition.z, 1));
        }
    }

    private Material[] FindMaterialsWithShader(Shader shader)
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        List<Material> results = new List<Material>();

        foreach(Renderer rend in renderers)
        {
            foreach(Material mat in rend.sharedMaterials)
            {
                if(mat.shader == timeshiftShader)
                {
                    results.Add(mat);
                }
            }
        }

        return results.ToArray();
    }
}
