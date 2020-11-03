using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlanter : MonoBehaviour
{
    private GameObject currentTree;
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;

    void Start()
    {
        SetCurrentTree(tree1);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(currentTree, transform.position, transform.rotation);
        }

        if(Input.GetKeyDown("1"))
        {
            SetCurrentTree(tree1);
        }

        if (Input.GetKeyDown("2"))
        {
            SetCurrentTree(tree2);
        }

        if (Input.GetKeyDown("3"))
        {
            SetCurrentTree(tree3);
        }
    }

    private void SetCurrentTree(GameObject tree)
    {
        currentTree = tree;

        HideAllTrees();

        currentTree.SetActive(true);
    }

    private void HideAllTrees()
    {
        tree1.SetActive(false);
        tree2.SetActive(false);
        tree3.SetActive(false);
    }
}
