using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaBar : MonoBehaviour {

    [SerializeField]
    private RectTransform self;

    private float height;

    // Update is called once per frame
    void Update()
    {
        height = (Population.numFleas * 92) / 20;
        self.sizeDelta = new Vector2(self.sizeDelta.x, height);
    }
}
