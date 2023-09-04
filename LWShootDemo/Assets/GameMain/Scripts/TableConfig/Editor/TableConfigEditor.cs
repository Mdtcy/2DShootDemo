#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace GameMain
{
    public abstract class TableConfigEditor<T, TList> : OdinMenuEditorWindow where T : IDProp where TList : SOTableList<T>
    {
        public abstract string LoadDataPath { get; }
        
        public abstract string SaveDataPath { get; }

        public abstract string NameFormat { get; }
        
        private List<T> _tmpList = new();

        public string NewName(T prop)
        {
            return string.Format(NameFormat, prop.DefaultName, prop.ID);
        }

        private TList _tableList;

        public TList TableList
        {
            get
            {
                if (_tableList == null)
                {
                    _tableList = TableConfigEditorUtility.GetTableInEditor<TList>();
                }

                return _tableList;

            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;
            tree.Config.DrawSearchToolbar = true;
            tree.Add("创建新配置", new CreateNewProp<T>(tree, TableList, SaveDataPath, NewName));
            tree.Add("Table", TableList);

            _tmpList.Clear();
            foreach (var prop in TableList.TableList)
            {
                string pattern = "【(.*?)】"; // 匹配【xx】模式的正则表达式 只匹配第一个
                Match match = Regex.Match(prop.name, pattern);
                if (match.Success) // 如果匹配成功
                {
                    string submenuName = match.Groups[1].Value; // 获取xx部分
                    tree.Add($"Table/{submenuName}/{prop.name}", prop); // 添加到相应的子菜单
                }
                else // 如果没有匹配到【xx】模式
                {
                    _tmpList.Add(prop); // 先添加到临时列表
                }
            }

            foreach (var prop in _tmpList)
            {
                tree.Add($"Table/{prop.name}", prop); // 正常添加   
            }

            return tree;
        }
        
        protected override void OnBeginDrawEditors()
        {
            OdinMenuTreeSelection selected = this.MenuTree.Selection;

            if (selected.SelectedValue is T prop)
            {
                SirenixEditorGUI.BeginHorizontalToolbar();
                {
                    GUILayout.FlexibleSpace();
                    
                    // 复制一份配置，并添加到 TableList 中
                    if (SirenixEditorGUI.ToolbarButton("复制"))
                    {
                        TableConfigEditorUtility.CopyProp(prop, TableList);
                        MenuTree.MarkDirty();
                    }

                    // 点击按钮后，更改 Prop 的名字
                    if (SirenixEditorGUI.ToolbarButton("默认名覆盖资源名"))
                    {
                        TableConfigEditorUtility.RenamePropWithDefaultName(prop);
                        MenuTree.MarkDirty();
                    }
                    
                    // 点击按钮后，更改 Prop 的名字
                    if (SirenixEditorGUI.ToolbarButton("资源名覆盖默认名"))
                    {
                        TableConfigEditorUtility.RenameDefaultNameWithPropName(prop);
                        MenuTree.MarkDirty();
                    }

                    if (SirenixEditorGUI.ToolbarButton("删除"))
                    {
                        TableConfigEditorUtility.DeleteProp(prop, TableList);
                        MenuTree.MarkDirty();
                    }

                }
                SirenixEditorGUI.EndHorizontalToolbar();    
            }
            else if(selected.SelectedValue is TList tList)
            {
                SirenixEditorGUI.BeginHorizontalToolbar();
                GUILayout.FlexibleSpace();
                
                if (SirenixEditorGUI.ToolbarButton("重置所有ID"))
                {
                    // 弹出一个对话框，询问用户是否确定要执行这个操作
                    if (EditorUtility.DisplayDialog("重置所有ID", "确定要重置所有ID吗？请先做好备份", "确定", "取消"))
                    {
                        tList.ResetAllIds(NewName);
                        MenuTree.MarkDirty();
                    }
                }

                if (SirenixEditorGUI.ToolbarButton("收集所有配置"))
                {
                    tList.ColleteProps(LoadDataPath);
                    MenuTree.MarkDirty();
                }
                SirenixEditorGUI.EndHorizontalToolbar();
            }
        }
    }
    public class CreateNewProp<T> where T : IDProp
    {
        private OdinMenuTree _odinMenuTree;
        private SOTableList<T> _tableList;
        private string _dataPath;
        private Func<T,string> _renameFunc;

        public CreateNewProp(OdinMenuTree odinMenuTree, SOTableList<T> tableList, string dataPath, Func<T,string> renameFunc)
        {
            _odinMenuTree = odinMenuTree;
            _tableList = tableList;
            _dataPath = dataPath;
            _renameFunc = renameFunc;
            prop = ScriptableObject.CreateInstance<T>();
        }

        [InlineEditor(Expanded = true)]
        public T prop;

        [Button("创建")]
        private void CreateNewData()
        {
            int newID = _tableList.NewId();
            prop.ID = newID;
            AssetDatabase.CreateAsset(prop, _dataPath + "/" + _renameFunc(prop) + ".asset");
            _tableList.TableList.Add(prop);
            
            EditorUtility.SetDirty(prop);
            EditorUtility.SetDirty(_tableList);

            AssetDatabase.SaveAssets();
            _odinMenuTree.MarkDirty();
        }
    }
}

#endif