using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//[CustomEditor(typeof(BeatAttack))]
public class BeatAttackEditor : EditorWindow
{
    private BeatAttack _beatAttack;
    private VisualElement _rootElement;
    private VisualTreeAsset _visualTree;
    private VisualElement ContainerVlement;
    private StyleSheet uss;
    
    private void OnEnable()
    {
        var _rootElement = rootVisualElement;
        
        
        //_beatAttack = target as BeatAttack;
        //_rootElement = new VisualElement();
        _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/BeatAttack.uxml");
        _visualTree.CloneTree(_rootElement);
        
        
        uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/BeatAttack.uss");                               
        _rootElement.styleSheets.Add(uss);
        
        
        //Debug.Log(typeof(BeatAttack).AssemblyQualifiedName);
        
        var objectFields = _rootElement.Query<ObjectField>();
        objectFields.ForEach(SetupObject);
        
        
        ContainerVlement = new VisualElement();
        _rootElement.Add(ContainerVlement);
        
    }

    private void SetupObject(ObjectField obj)
    {
        if (obj.name == "BeatAttackOBJ")
        {
            Debug.Log("Success " + obj.value);
            obj.RegisterValueChangedCallback(ObjChangedCallback);
        }
    }

    private void ObjChangedCallback(ChangeEvent<Object> evt)
    {

        Debug.Log("ValueChanged " + evt.newValue);
        
        _beatAttack = evt.newValue as BeatAttack;
        
        ContainerVlement.Clear();
        
        int i = 0;
        foreach (BeatAttack.BeatDamageProperties damagePropertiese in _beatAttack.Damages)
        {

            string beat = i++.ToString();
            VisualElement element = new VisualElement();
            element.Add(new  Label("Beat"+beat));
            
            ContainerVlement.Add(element);
        }
        
        
        
    }


    [MenuItem("BeatAttack/Open _%#T")]
    public static void ShowWindow()
    {
        // Opens the window, otherwise focuses it if it’s already open.
        var window = GetWindow<BeatAttackEditor>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("A Beat Attack Editor");

        // Sets a minimum size to the window.
        window.minSize = new Vector2(250, 50);
    }
    
    
    
    
  //public override VisualElement CreateInspectorGUI()
    public VisualElement CreateInspectorGUI()
    {

        //var rootOrig = base.CreateInspectorGUI();

        
        
        var root = _rootElement;
        root.Clear();


        _visualTree.CloneTree(root);
        
        //Debug.Log(rootOrig);
        //root.Add(rootOrig);
        
        
        VisualElement ContainerVlement = new VisualElement();
        
        int i = 0;
        foreach (BeatAttack.BeatDamageProperties damagePropertiese in _beatAttack.Damages)
        {

            string beat = i++.ToString();
            VisualElement element = new VisualElement();
            element.Add(new  Label("Beat"+beat));
            
            ContainerVlement.Add(element);
        }
        root.Add(ContainerVlement);
        
        return root;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
