using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public RectTransform box;
    Vector2 startPos;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            // RaycastHit hit;
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //
            // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 10);
            //
            // if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Square")))
            // {
            //     
            //     Debug.Log(hit.collider.gameObject.name);
            // }
        }

        if (Input.GetMouseButton(0))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                foreach (ThisIsSquare i in FindObjectsByType<ThisIsSquare>(FindObjectsSortMode.InstanceID))
                {
                    i.Deselect();
                }
                squares.Clear();
            }
            UpdateBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }
    }

    void UpdateBox(Vector2 mousePos)
    {
        if (!box.gameObject.activeSelf)
        {
            box.gameObject.SetActive(true);
        }

        float w = mousePos.x - startPos.x;
        float h = mousePos.y - startPos.y;

        box.sizeDelta = new Vector2(Mathf.Abs(w), Mathf.Abs(h));
        box.anchoredPosition = startPos + new Vector2(w / 2, h / 2);
    }

    private List<ThisIsSquare> squares = new List<ThisIsSquare>();

    void Release()
    {
        box.gameObject.SetActive(false);

        Vector2 min = box.anchoredPosition - (box.sizeDelta / 2);
        Vector2 max = box.anchoredPosition + (box.sizeDelta / 2);

        foreach (ThisIsSquare i in FindObjectsByType<ThisIsSquare>(FindObjectsSortMode.InstanceID))
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(i.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                if (!squares.Contains(i))
                {
                    squares.Add(i);
                    i.Select();
                }
                else if (Input.GetKey(KeyCode.LeftControl) && squares.Contains(i))
                {
                    squares.Remove(i);
                    i.Deselect();
                }
            }
        }
    }
}
