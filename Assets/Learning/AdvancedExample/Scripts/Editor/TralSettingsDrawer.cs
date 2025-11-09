// using UnityEngine;
// using UnityEditor;
//
// [CustomPropertyDrawer(typeof(TrailSettings))]
// public class TrailSettingsDrawer : PropertyDrawer {
//     private const int lineHeight = 30;
//     private const int spacing = 7;
//     private const int textHeight = 16; // Desired font size
//
//     private GUIStyle labelStyle;
//     private GUIStyle fieldStyle;
//
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//         // Initialize styles if null
//         if(labelStyle == null) {
//             labelStyle = new GUIStyle(EditorStyles.label);
//             labelStyle.fontSize = textHeight; // Set desired font size
//         }
//
//         if(fieldStyle == null) {
//             fieldStyle = new GUIStyle(EditorStyles.textField);
//             fieldStyle.fontSize = textHeight;
//         }
//
//         // Begin property and set indent level
//         EditorGUI.BeginProperty(position, label, property);
//         int originalIndentLevel = EditorGUI.indentLevel;
//
//         // Draw the foldout with custom label style
//         property.isExpanded = EditorGUI.Foldout(
//             new Rect(position.x, position.y, position.width, lineHeight),
//             property.isExpanded,
//             label,
//             true,
//             labelStyle
//         );
//
//         if(property.isExpanded) {
//             EditorGUI.indentLevel++;
//
//             // Retrieve properties
//             SerializedProperty colorProp = property.FindPropertyRelative("color");
//             SerializedProperty widthProp = property.FindPropertyRelative("width");
//             SerializedProperty lengthProp = property.FindPropertyRelative("length");
//
//             float yOffset = position.y + lineHeight + spacing;
//
//             // Color field
//             Rect colorRect = new Rect(position.x, yOffset, position.width, lineHeight);
//             DrawPropertyWithStyle(colorRect, colorProp, labelStyle, fieldStyle);
//
//             // Width field
//             yOffset += lineHeight + spacing;
//             Rect widthRect = new Rect(position.x, yOffset, position.width, lineHeight);
//             DrawPropertyWithStyle(widthRect, widthProp, labelStyle, fieldStyle);
//
//             // Length field
//             yOffset += lineHeight + spacing;
//             Rect lengthRect = new Rect(position.x, yOffset, position.width, lineHeight);
//             DrawPropertyWithStyle(lengthRect, lengthProp, labelStyle, fieldStyle);
//         }
//
//         // Restore indent level and end property
//         EditorGUI.indentLevel = originalIndentLevel;
//         EditorGUI.EndProperty();
//     }
//
//     private void DrawPropertyWithStyle(Rect position, SerializedProperty property, GUIStyle labelStyle, GUIStyle fieldStyle) {
//         // Draw the label with custom style
//         Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
//         EditorGUI.LabelField(labelRect, new GUIContent(property.displayName), labelStyle);
//
//         // Draw the field with custom style
//         Rect fieldRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);
//
//         switch(property.propertyType) {
//             case SerializedPropertyType.Color:
//                 property.colorValue = EditorGUI.ColorField(fieldRect, property.colorValue);
//                 break;
//             case SerializedPropertyType.Float:
//                 property.floatValue = EditorGUI.FloatField(fieldRect, property.floatValue, fieldStyle);
//                 break;
//             case SerializedPropertyType.Integer:
//                 property.intValue = EditorGUI.IntField(fieldRect, property.intValue, fieldStyle);
//                 break;
//             // Add other property types if needed
//             default:
//                 EditorGUI.PropertyField(fieldRect, property, GUIContent.none);
//                 break;
//         }
//     }
//
//     public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
//         int lines = 1; // Start with one line for the foldout
//
//         if(property.isExpanded) {
//             lines += 3; // Add one line for each property field
//         }
//
//         return lines * (lineHeight + spacing);
//     }
// }
