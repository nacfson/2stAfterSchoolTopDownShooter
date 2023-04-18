using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorInHierachy : MonoBehaviour{

    #if UNITY_EDITOR
    private static Dictionary<Object,ColorInHierachy> _coloredObjects = new Dictionary<Object, ColorInHierachy>();

    public string prefix;
    public Color backColor;
    public Color fontColor;

    public static void PaintObject(Object obj, Rect selectionRect, ColorInHierachy cih){
        Rect bgRect = new Rect(selectionRect.x,selectionRect.y,selectionRect.width + 50, selectionRect.height);

        if(Selection.activeObject !=obj){
            EditorGUI.DrawRect(bgRect,cih.backColor);

            string name = $"{cih.prefix} {obj.name}";

            EditorGUI.LabelField(bgRect,name,new GUIStyle(){
                normal = new GUIStyleState() {textColor = cih.fontColor},
                fontStyle = FontStyle.Bold,
            });
        }
    }


    static ColorInHierachy(){
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect){
        Object obj = EditorUtility.InstanceIDToObject(instanceID);

        if(obj != null && _coloredObjects.ContainsKey(obj)){
            GameObject monoObj = obj as GameObject;
            ColorInHierachy cih = monoObj.GetComponent<ColorInHierachy>();
            if(cih != null){
                PaintObject(obj,selectionRect,cih);
            }
            else{
                _coloredObjects.Remove(obj);
            }
        }
    }
    //Reset 메시지는 에디터 전용 Editor에서만 실행
    public void Reset(){
        OnValidate();
    }

    private void OnValidate() {
        if(_coloredObjects.ContainsKey(this.gameObject) == false){
            _coloredObjects.Add(this.gameObject,this);
        }

    }
#endif
}