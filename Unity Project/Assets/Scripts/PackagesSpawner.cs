using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagesSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject PackagePrefab;

    [SerializeField]
    public int AmountOfPackages;

    public GameObject[] Packages;

    void FixedUpdate()
    {
        List<GameObject> PackageList = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.tag == "PackageParent" && child != null)
                PackageList.Add(child.GetChild(0).gameObject);
        }

        Packages = PackageList.ToArray();

        if (Packages.Length + 1 <= AmountOfPackages)
            InstantiatePackage();
    }

    public void resetPackages()
    {
        for (int i = 0; i < Packages.Length; i++)
        {
            if (Packages[i] != null)
            {
                Destroy(Packages[i].transform.parent.gameObject);
            }
        }

        for (int i = 0; i < AmountOfPackages; i++)
            InstantiatePackage();
    }

    public void InstantiatePackage()
    {
        GameObject package = Instantiate(PackagePrefab, new Vector3(transform.position.x + Random.Range(-450, 450), transform.position.y + Random.Range(100, 950), transform.position.z + Random.Range(-450, 450)), Quaternion.identity);

        package.transform.parent = transform;
    }
}
