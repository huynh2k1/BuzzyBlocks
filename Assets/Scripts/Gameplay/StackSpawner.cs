using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class StackSpawner : MonoBehaviour
{
    public static StackSpawner I;

    [Header("ELEMENTS")]
    public Transform stackPositionsParent;
    public Hexagon hexaPrefab;
    public HexaStack hexaStackPrefab;

    [Header("SETTINGS")]
    [SerializeField] private Color[] colors;
    [SerializeField] private int stackCounter;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        I = this;
        InputCtrl.OnStackPlace += StackPlaceCallback;
    }

    private void OnDestroy()
    {
        InputCtrl.OnStackPlace -= StackPlaceCallback;
    }

    private void StackPlaceCallback(HexaCell hexaCell)
    {
        stackCounter++;
        if (stackCounter >= 3)
        {
            GenerateStacks();
        }
    }

    public void GenerateStacks()
    {
        stackCounter = 0;
        for (int i = 0; i < stackPositionsParent.childCount; i++)
        {
            GenerateStack(stackPositionsParent.GetChild(i));
        }
    }

    public void SpawnStacks()
    {
        // Xoá tất cả stack con
        for (int i = 0; i < stackPositionsParent.childCount; i++)
        {
            Transform pos = stackPositionsParent.GetChild(i);

            // Xoá tất cả HexaStack đang tồn tại trong mỗi vị trí
            foreach (Transform child in pos)
            {
                Destroy(child.gameObject);
            }
        }

        // Tạo stack mới
        GenerateStacks();
    }

    void GenerateStack(Transform parent)
    {
        HexaStack hexaStack = Instantiate(hexaStackPrefab, parent.position, Quaternion.identity, parent);
        hexaStack.name = $"Stack {parent.GetSiblingIndex()}";


        int amount = Random.Range(2, 8); //return 2 -> 7
        int firstColorHexagonCount = Random.Range(0, amount); //return 0, 6
        Color[] colorArray = GetRandomColors();
        for (int i = 0; i < amount; i++)
        {
            Vector3 hexaLocalPos = Vector3.up * i * 0.1f;
            Vector3 spawnPos = hexaStack.transform.TransformPoint(hexaLocalPos);
            Hexagon hexa = Instantiate(hexaPrefab, hexaStack.transform);
            hexa.transform.position = spawnPos;
            hexa.Color = (i < firstColorHexagonCount) ? colorArray[0] : colorArray[1];
            hexaStack.Add(hexa);
            hexa.Configure(hexaStack);
        }
    }

    Color[] GetRandomColors()
    {
        List<Color> colorList = new List<Color>();
        colorList.AddRange(colors);

        if (colorList.Count <= 0)
        {
            Debug.LogError("No color found");
            return null;
        }

        Color firstColor = colorList.OrderBy(x => Random.value).First();
        colorList.Remove(firstColor);

        if (colorList.Count <= 0)
        {
            Debug.LogError("Only one color was found");
            return null;
        }

        Color secondColor = colorList.OrderBy(x => Random.value).First();

        return new Color[] { firstColor, secondColor };
    }
}
