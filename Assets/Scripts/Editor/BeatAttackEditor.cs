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
    private VisualTreeAsset _visualTreeBeat;
    private VisualElement ContainerVlement;
    private VisualElement BeatsContainer;
    private StyleSheet uss;
    
    private void OnEnable()
    {
        var _rootElement = rootVisualElement;
        
        
        //_beatAttack = target as BeatAttack;
        //_rootElement = new VisualElement();
        _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/BeatAttack.uxml");
        _visualTreeBeat = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/BeatUiElement.uxml");
        _visualTree.CloneTree(_rootElement);
        
        
        uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/BeatAttack.uss");                               
        _rootElement.styleSheets.Add(uss);
        
        
        
        
        var objectFields = _rootElement.Query<ObjectField>();
        objectFields.ForEach(SetupObject);
        
        
        ContainerVlement = new VisualElement();
        _rootElement.Add(ContainerVlement);

        BeatsContainer = _rootElement.Q<VisualElement>("BeatsContainer");

    }

    private void SetupObject(ObjectField obj)
    {
        if (obj.name == "BeatAttackOBJ")
        {
            //Debug.Log("Success " + obj.value);
            obj.RegisterValueChangedCallback(ObjChangedCallback);
        }
    }

    private void ObjChangedCallback(ChangeEvent<Object> evt)
    {

        Debug.Log("ValueChanged " + evt.newValue);
        
        _beatAttack = evt.newValue as BeatAttack;        
        
        ContainerVlement.Clear();
        BeatsContainer.Clear();
        
        int i = 0;
        foreach (BeatAttack.BeatDamageProperties damagePropertiese in _beatAttack.Damages)
        {

            VisualElement beatEelement = CreateBeatElement(damagePropertiese,i);
            ProgressBar progress = beatEelement.Q<ProgressBar>();
            float progressVal = 100f * i / _beatAttack.Damages.Length ;

            progress.value = progressVal; 
            
            

            
            
            
            // TODO: Reogranize this
            VisualElement element = new VisualElement();            
            element.Add(beatEelement);            
            ContainerVlement.Add(element);
            BeatsContainer.Add(element);


            i++;
        }
        
        
        
    }


    VisualElement CreateBeatElement(BeatAttack.BeatDamageProperties damagePropertiese, int beatNum)
    {
        VisualElement element = new VisualElement();
        _visualTreeBeat.CloneTree(element);
        /*
        element.Add(new Label(damagePropertiese._damageType.ToString()));                
        element.Add(new Label("Damage Type"));
        

        element.Add(new Label(damagePropertiese.Strentgh.ToString()));                
        element.Add(new Label("Strentgh"));
        */
        
        string beat = beatNum.ToString();
        element.Q<Label>("BeatNum").text = "Beat " + beat;

        EnumField damageType = element.Q<EnumField>("DamageType");        
        damageType.value = damagePropertiese._damageType;
        
        FloatField stength = element.Q<FloatField>("strength");        
        //stength.value = damagePropertiese.Strentgh;

        SerializedObject so = new  SerializedObject(_beatAttack);
        Debug.Log("So" + so);
        SerializedObject so1 = new  SerializedObject(_beatAttack.Damages);
        SerializedProperty sp =so.FindProperty("Damages[0].Strentgh");
        Debug.Log("Sp" + sp);
        stength.BindProperty(sp);

        //Debug.Log(typeof(Sprite).AssemblyQualifiedName);
        ObjectField spriteField= element.Q<ObjectField>("Sprite");        
        spriteField.value = damagePropertiese.sprite;
        
        
        return element;
        
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
