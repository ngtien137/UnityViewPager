using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPager : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;

    [SerializeField]
    private int pageCount;

    private RectTransform rectTransform;

    [SerializeField]
    private Scrollbar scrollBar;

    private float scrollPosition = 0;
    private float[] positions;

    private Transform panelParent;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        panelParent = gridLayoutGroup.gameObject.transform;
        gridLayoutGroup.constraintCount = pageCount;
        gridLayoutGroup.cellSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }


    private void Update()
    {
        positions = new float[pageCount];
        float distance = 1f / (positions.Length - 1f);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scrollPosition = scrollBar.value;
        }
        else
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (scrollPosition < positions[i] + (distance/2) && scrollPosition > positions[i] - distance / 2)
                {
                    scrollBar.value = Mathf.Lerp(scrollBar.value, positions[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < positions.Length; i++)
        {
            if (scrollPosition < positions[i] + distance/2 && scrollPosition > positions[i] - distance / 2f)
            {
                try
                {
                    panelParent.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                    for (int a = 0; a < positions.Length; a++)
                    {
                        if (a != i)
                        {
                            panelParent.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        }
                    }
                }
                catch (System.Exception e)
                {

                }
            }
        }
    }
}
