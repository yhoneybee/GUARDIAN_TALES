using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Editor ȯ�濡�� Guardian �ν����Ϳ� Weapon�� �Ҵ��� �� Weapon Type�� �ٸ��� �������� ���ϰ� ��


[CustomPropertyDrawer(typeof(WeaponEquipAttribute), true)]
public class WeaponEquipAttributeDrawer : PropertyDrawer
{
    private WeaponEquipAttribute Atr => attribute as WeaponEquipAttribute;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        if (property.objectReferenceValue != null)
        {
            var weapon = property.objectReferenceValue as Weapon;

            if (Atr.WeaponType != weapon.WeaponType)
            {
                property.objectReferenceValue = null;
            }
        }

    }
}
