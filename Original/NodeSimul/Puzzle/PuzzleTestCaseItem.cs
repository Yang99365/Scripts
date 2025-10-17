using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

public class PuzzleTestCaseItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI caseNumberText;
    [SerializeField] private Transform inputContainer;
    [SerializeField] private Transform outputContainer;
    [SerializeField] private GameObject inputTogglePrefab;
    [SerializeField] private GameObject outputTogglePrefab;

    [SerializeField] private GameObject resultImageObject;
    [SerializeField] private Sprite passedSprite;
    [SerializeField] private Sprite failedSprite;

    [SerializeField] private Color passedColor = Color.white;
    [SerializeField] private Color failedColor = Color.red;


    private List<Toggle> inputToggles = new List<Toggle>();
    private List<Toggle> outputToggles = new List<Toggle>();
    public void SetupTestCase(TestCase testCase, int index)
    {
        caseNumberText.text = $"Case #{index + 1}";
        caseNumberText.color = passedColor;

        // �� �׽�Ʈ ���̽� ���� �� ��� �̹��� �ʱ�ȭ �� ����
        if (resultImageObject != null)
        {
            resultImageObject.SetActive(false);
        }

        ClearToggles();


        if (testCase.ExternalInputStates != null)
        {
            for (int i = 0; i < testCase.ExternalInputStates.Count; i++)
            {
                Toggle toggle = CreateToggle(inputContainer, $"IN {i}", testCase.ExternalInputStates[i], true);
                inputToggles.Add(toggle);
            }
        }

        // Create output toggles
        if (testCase.ExternalOutputStates != null)
        {
            for (int i = 0; i < testCase.ExternalOutputStates.Count; i++)
            {
                Toggle toggle = CreateToggle(outputContainer, $"OUT {i}", testCase.ExternalOutputStates[i], false);
                outputToggles.Add(toggle);
            }
        }
    }

    private Toggle CreateToggle(Transform parent, string label, bool isOn, bool isInput)
    {
        GameObject prefab = isInput ? inputTogglePrefab : outputTogglePrefab;
        GameObject toggleObj = Instantiate(prefab, parent);
        Toggle toggle = toggleObj.GetComponent<Toggle>();
        Text labelText = toggle.GetComponentInChildren<Text>();

        if (toggle != null)
        {
            // Input ����� ���� �״�� ����
            if (isInput)
            {
                toggle.isOn = isOn;
            }
            // Output ����� �׻� ���� ���·� ����
            else
            {
                toggle.isOn = false;

                // Effect �̹����� ���� �ڽ� �߿����� ã��
                Transform effectTransform = null;
                for (int i = 0; i < toggleObj.transform.childCount; i++)
                {
                    Transform child = toggleObj.transform.GetChild(i);
                    if (child.name == "Effect")
                    {
                        effectTransform = child;
                        break;
                    }
                }

                if (effectTransform != null)
                {
                    // ���� ���� �α�
                    Debug.Log($"Output toggle '{label}': Setting Effect active = {isOn}");

                    // Ȱ��ȭ ���� ����
                    effectTransform.gameObject.SetActive(isOn);

                }

            }

            toggle.interactable = false;
        }

        if (labelText != null)
        {
            labelText.text = label;
        }

        return toggle;
    }

    private void ClearToggles()
    {
        foreach (var toggle in inputToggles)
        {
            Destroy(toggle.gameObject);
        }
        inputToggles.Clear();

        foreach (var toggle in outputToggles)
        {
            Destroy(toggle.gameObject);
        }
        outputToggles.Clear();
    }

    public void SetValidationResult(bool passed)
    {
        caseNumberText.color = passed ? passedColor : failedColor;

        if (resultImageObject != null)
        {
            // ����� ���� ��������Ʈ
            Image resultImage = resultImageObject.GetComponent<Image>();
            if (resultImage != null)
            {
                // ����� ���� ������ ��������Ʈ ����
                resultImage.sprite = passed ? passedSprite : failedSprite;
            }

            // ��� ǥ��
            resultImageObject.SetActive(true);
            resultImageObject.GetComponent<Image>().color = passed ? passedColor : failedColor;

            // ��ƼŬ ȿ�� �߰�����
        }
    }
    public void ResetResult()
    {
        // ��� �̹��� �ʱ�ȭ �� ����
        if (resultImageObject != null)
        {
            resultImageObject.SetActive(false);
        }

        // �ؽ�Ʈ ���� �ʱ�ȭ
        caseNumberText.color = passedColor;

        // ��� ��� �ʱ�ȭ
        foreach (var toggle in outputToggles)
        {
            toggle.isOn = false;
        }
    }
    public void SetDetailedValidationResult(bool[] actualOutputs, bool passed)
    {

        // ���� ��°��� ���� ��� ���¸� ������Ʈ�ϰ�, Effect�� �������� ����
        for (int i = 0; i < actualOutputs.Length && i < outputToggles.Count; i++)
        {
            outputToggles[i].isOn = actualOutputs[i];

            
        }
    }
}