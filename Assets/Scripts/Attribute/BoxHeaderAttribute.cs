using UnityEngine;


[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class BoxHeaderAttribute : PropertyAttribute
{
    public string HeaderText { get; private set; } = "";

    // �� ���� �ʵ带 ���� ������ ����
    public int FieldCount { get; private set; } = 0;

    public Color HeaderColor { get; set; } = Color.white;
    public Color BoxColor { get; set; } = Color.black;

    public float Alpha { get; set; } = 0.4f;

    // �߰� �ϴ� ����
    public float BottomHeight { get; set; } = 0f;

    // ���� ���� ������ �� �ְ� ���ִ� ���� ���� ���� ����
    //public bool UseColorPicker { get; set; } = false;

    public BoxHeaderAttribute(string header, int fieldCount)
    {
        HeaderText = header;
        FieldCount = fieldCount;
    }
}