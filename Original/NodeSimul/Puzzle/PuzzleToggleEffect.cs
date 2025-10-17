using UnityEngine;
using UnityEngine.UI;

public class PuzzleToggleEffect : MonoBehaviour
{
    [SerializeField]
    private Image m_ToggleImage;
    private Color defaultColor;

    [SerializeField]
    private float m_blinkSpeed = 2f; // �����̴� �ӵ� ����

    [SerializeField]
    private float m_minAlpha = 0.3f; // �ּ� ���İ� (������ �������� �ʰ�)

    [SerializeField]
    private float m_maxAlpha = 1f; // �ִ� ���İ�
    private void Start()
    {
        defaultColor = m_ToggleImage.color;
    }
    void Update()
    {
        // Time.time�� ����� �ð� ������� Sin �Լ� ���
        float alpha = Mathf.Sin(Time.time * m_blinkSpeed);

        // Sin ���� 0~1 ������ ����ȭ (Sin�� -1~1 ���� �����Ƿ�)
        alpha = (alpha + 1f) / 2f;

        // �ּҰ��� �ִ밪 ���̷� ���İ� ����
        alpha = Mathf.Lerp(m_minAlpha, m_maxAlpha, alpha);

        // ���ο� ���� ���� (Color�� struct�̹Ƿ� ���� ���� �Ұ�, ���� �Ҵ� �ʿ�)
        Color newColor = defaultColor;
        newColor.a = alpha;
        m_ToggleImage.color = newColor;
    }

    
}
