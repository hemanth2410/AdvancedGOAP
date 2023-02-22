using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.Rendering.HighDefinition;

public class GoalPrioritizerEditor : EditorWindow
{

    private MultiColumnHeaderState _multiColoumnHeaderState;
    private MultiColumnHeader _multiColoumnHeader;
    private MultiColumnHeaderState.Column[] _coloumns;
    private float _multiColoumnHeaderWidth;
    private bool firstOnGUIIterationAfterInitialize;
    string NpcAgent = "";
    private Vector2 _scrollPosition;

    [MenuItem("Advanced GOAP/Goal Prioritization window")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesnt exist make one.
        EditorWindow.GetWindow<GoalPrioritizerEditor>(title: "Goal Prioritization window" ,focus : true);
    }
    private void OnGUI()
    {
        GUILayout.Label("GOAP Settings", EditorStyles.boldLabel);
        NpcAgent = EditorGUILayout.TextField("NpcAgent", Selection.activeGameObject.name);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Each GOAP agent will have 100 units and these units will be distributed equally among all the goals", EditorStyles.boldLabel);
        // Define headings
        float columnHeight = EditorGUIUtility.singleLineHeight;
        GUILayout.FlexibleSpace();
        Rect windowRect = GUILayoutUtility.GetLastRect();
        windowRect.width = position.width;
        windowRect.height = position.height;
        if (_multiColoumnHeader == null)
        {
            this.Initilize();
        }
        GUIStyle groupGUIStyle = new GUIStyle(GUI.skin.box);
        Vector2 groupRectPaddingInWIndow = new Vector2(20.0f, 20.0f);
        Rect groupRect = new Rect(windowRect);
        groupRect.x += groupRectPaddingInWIndow.x;
        groupRect.width -= groupRectPaddingInWIndow.x * 2;
        groupRect.y += groupRectPaddingInWIndow.y;
        groupRect.height -= groupRectPaddingInWIndow.y * 2;
        GUI.BeginGroup(groupRect, groupGUIStyle);
        {
            groupRect.x -= groupRectPaddingInWIndow.x;
            groupRect.y -= groupRectPaddingInWIndow.y;

            Rect positionalRectAreaOfScrollView = new Rect(source: groupRect);

            // Create a `viewRect` since it should be separate from `rect` to avoid circular dependency.
            Rect viewRect = new Rect(source: groupRect)
            {
                width = this._multiColoumnHeaderState.widthOfAllVisibleColumns, // Scroll max on X is basically a sum of width of columns.
                //? Do not remove this hegiht. It's compensating for the size of bottom scroll slider when it appears, that is why the right side scroll slider appears.
                //height = groupRect.height - columnHeight, // Remove `columnHeight` - basically size of header.
            };

            groupRect.width += groupRectPaddingInWIndow.x * 2;
            groupRect.height += groupRectPaddingInWIndow.y * 2;

            this._scrollPosition = GUI.BeginScrollView(
                position: positionalRectAreaOfScrollView,
                scrollPosition: this._scrollPosition,
                viewRect: viewRect,
                alwaysShowHorizontal: false,
                alwaysShowVertical: false
            );
            {   // Scroll View Scope.

                //? After debugging for a few hours - this is the only hack I have found to actually work to aleviate that scaling bug.
                this._multiColoumnHeaderWidth = Mathf.Max(positionalRectAreaOfScrollView.width + this._scrollPosition.x, this._multiColoumnHeaderWidth);

                // This is a rect for our multi column table.
                Rect columnRectPrototype = new Rect(source: positionalRectAreaOfScrollView)
                {
                    width = this._multiColoumnHeaderWidth,
                    height = columnHeight, // This is basically a height of each column including header.
                };

                // Draw header for columns here.
                this._multiColoumnHeader.OnGUI(rect: columnRectPrototype, xScroll: 0.0f);

                float heightJump = columnHeight;

                // For each element that we have in object that we are modifying.
                //? I don't have an appropriate object here to modify, but this is just an example. In real world case I would probably use ScriptableObject here.

            }
            GUI.EndScrollView(handleScrollWheel: true);
        }
        GUI.EndGroup();
        this.firstOnGUIIterationAfterInitialize = false;
    }
    private void Initilize()
    {
        this.firstOnGUIIterationAfterInitialize = true;
        this._multiColoumnHeaderWidth = this.position.width;
        this._coloumns = new MultiColumnHeaderState.Column[]
        {
            new MultiColumnHeaderState.Column()
            {
                allowToggleVisibility = false,
                autoResize = true,
                minWidth = 100,
                maxWidth = 250,
                canSort = true,
                sortingArrowAlignment = TextAlignment.Right,
                headerContent = new GUIContent("Goal","Goal of the NPC"),
                headerTextAlignment = TextAlignment.Center,
            },
            new MultiColumnHeaderState.Column()
            {
                allowToggleVisibility = false,
                autoResize = true,
                minWidth = 100,
                maxWidth = 250,
                canSort = true,
                sortingArrowAlignment = TextAlignment.Right,
                headerContent = new GUIContent("Priority","Priority of each goal, this is calculated automatically at runtime"),
                headerTextAlignment = TextAlignment.Center,
            },
            new MultiColumnHeaderState.Column()
            {
                allowToggleVisibility = false,
                autoResize = true,
                minWidth = 100,
                maxWidth = 250,
                canSort = true,
                sortingArrowAlignment = TextAlignment.Right,
                headerContent = new GUIContent("Triggers","What are the triggers that will change priorities of this perticular goal"),
                headerTextAlignment = TextAlignment.Center,
            },
            new MultiColumnHeaderState.Column()
            {
                allowToggleVisibility = false,
                autoResize = true,
                minWidth = 100,
                maxWidth = 250,
                canSort = true,
                sortingArrowAlignment = TextAlignment.Right,
                headerContent = new GUIContent("Priority changes","How much change will this trigger in priority"),
                headerTextAlignment = TextAlignment.Center,
            },
        };
        this._multiColoumnHeaderState = new MultiColumnHeaderState(_coloumns);
        this._multiColoumnHeader = new MultiColumnHeader(this._multiColoumnHeaderState);
        // When we chagne visibility of the column we resize columns to fit in the window.
        this._multiColoumnHeader.visibleColumnsChanged += (multiColumnHeader) =>
        {
            multiColumnHeader.ResizeToFit();
        };

        // Initial resizing of the content.
        this._multiColoumnHeader.ResizeToFit();
    }
    private void Awake()
    {
        Initilize();
    }
}
