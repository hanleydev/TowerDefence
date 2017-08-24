using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Build Manager in scene");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;

    private void Start()
    {
        turrentToBuild = standardTurretPrefab;
    }

    private GameObject turrentToBuild;

    public GameObject GetTurrentToBuild()
    {
        return turrentToBuild;
    }

}
