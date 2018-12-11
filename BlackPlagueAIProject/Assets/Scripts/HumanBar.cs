using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBar : MonoBehaviour {

    [SerializeField]
    private RectTransform self;

    private float height;

    // Update is called once per frame
    void Update()
    {
        height = ((Population.numUninfected + Population.numInfected) * 92) / 20;
        self.sizeDelta = new Vector2(self.sizeDelta.x, height);
    }
}
