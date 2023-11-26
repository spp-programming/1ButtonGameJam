using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeInput : MonoBehaviour
{
    EventSystem eventSystem;
    public Selectable[] Selectables;
    int currentSelectableIndex = 0;

    float DeselectThreshold = 18;

    bool isIdle;

    float idleTimer = 0;
    private const float IdleTimeThreshold = 10;

    private Vector2 lastMousePosition;

    // Start is called before the first frame update
    void Awake() => eventSystem = EventSystem.current;

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= IdleTimeThreshold)
            {
                DeselectAllElements();
                idleTimer = 0;
                isIdle = false;
            }
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            idleTimer = 0;
        }
        else
        {
            float distance = Vector2.Distance(lastMousePosition, Input.mousePosition);

            if (distance > DeselectThreshold)
            {
                DeselectAllElements();
                isIdle = true;
                currentSelectableIndex = 0;
            }
            lastMousePosition = Input.mousePosition;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            isIdle = true;
            idleTimer = 0;
            SelectElement(currentSelectableIndex);

            currentSelectableIndex++;
            if (currentSelectableIndex >= Selectables.Length)
                currentSelectableIndex = 0;
        }
    }

    void SelectElement(int index)
    {
        DeselectAllElements();
        eventSystem.SetSelectedGameObject(Selectables[index].gameObject);
    }

    public void DeselectAllElements() => eventSystem.SetSelectedGameObject(null);

    private void OnDisable()
    {
        DeselectAllElements();
        currentSelectableIndex = 0;
    }

    private void OnEnable() => DeselectAllElements();
}
