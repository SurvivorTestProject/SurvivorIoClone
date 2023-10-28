using Enums;
using Skills;
using StatsPackage;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatPropertyDrawer : PropertyDrawer
    {
        private int numberOfLines = 1;
        float lineHeight = EditorGUIUtility.singleLineHeight;
        private Rect currentPosition; 
        private SerializedProperty currentProperty;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            this.currentPosition = position;
            this.currentProperty = property;
            EditorGUI.BeginProperty(position, label, property);
            EditorGUIUtility.labelWidth = 70;
            SerializedProperty serializedProperty = GenerateProperty("statType",0,0);
            StatType statType = (StatType)serializedProperty.enumValueIndex;
            
            if (statType is StatType.childSprite or StatType.parentSprite)
            {
                GenerateProperty("sprite",0,0.5f);
            }else if (statType is StatType.description or StatType.name)
            {
                GenerateProperty("stringValue",0,0.5f);
            }
            else
            {
                GenerateProperty("value",0,0.5f);
            }
            EditorGUI.EndProperty();

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 1.2f;
        }

        private SerializedProperty GenerateProperty(string propertyName,int verticalPosition,float widthMultiplier)
        {
            Rect actionPosition = new Rect(currentPosition.x + (currentPosition.width*widthMultiplier), currentPosition.y + (lineHeight * verticalPosition * 1.2f), currentPosition.width/2, lineHeight);
            SerializedProperty actionProperty = currentProperty.FindPropertyRelative(propertyName);
            EditorGUI.PropertyField(actionPosition, actionProperty);
            return actionProperty;
        }
    }
}
