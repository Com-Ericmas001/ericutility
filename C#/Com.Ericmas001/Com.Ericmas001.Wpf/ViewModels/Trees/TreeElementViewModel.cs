using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using Com.Ericmas001.Wpf.ViewModels.Tabs;

namespace Com.Ericmas001.Wpf.ViewModels.Trees
{
    public abstract class TreeElementViewModel : BaseViewModel
    {
        public event NewTabEventHandler OnTabCreation = delegate { };
        private bool m_IsExpanded = false;

        public virtual FontWeight FontWeight { get { return FontWeights.Normal; } }
        public virtual FontFamily FontFamily { get { return new FontFamily("Microsoft Sans Serif"); } }
        public virtual FontStyle FontStyle { get { return FontStyles.Normal; } }
        public virtual Brush FontColor { get { return Brushes.Black; } }

        public string Path
        {
            get
            {
                return Parent == null ? null : (Parent.Path == null ? Parent.NameForPath : Parent.Path + " / " + Parent.NameForPath);
            }
        }

        public string PathAndName
        {
            get
            {
                return Path == null ? NameForPath : Path + " / " + NameForPath;
            }
        }

        public virtual string NameForPath { get { return Text; } }

        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }
        private FastObservableCollection<TreeElementViewModel> m_Children;
        public FastObservableCollection<TreeElementViewModel> Children
        {
            get
            {
                if (m_Children == null)
                {
                    m_Children = new FastObservableCollection<TreeElementViewModel>();
                }
                return m_Children;
            }
        }
        public TreeElementViewModel Parent { get; private set; }
        public abstract string Text { get; }

        private FastObservableCollection<TreeContextMenuItemViewModel> m_ContextMenuItems;
        public FastObservableCollection<TreeContextMenuItemViewModel> ContextMenuItems
        {
            get
            {
                if (m_ContextMenuItems == null)
                {
                    m_ContextMenuItems = new FastObservableCollection<TreeContextMenuItemViewModel>();
                    PopulateContextMenu();
                }
                return m_ContextMenuItems;
            }
        }

        public TreeElementViewModel(TreeElementViewModel parent)
        {
            Parent = parent;
        }

        public bool HasOnlyOneLeaf
        {
            get
            {
                return NbLeaves == 1;
            }
        }

        public TreeElementViewModel FirstLeaf
        {
            get
            {
                return TreeLeaves.FirstOrDefault();
            }
        }

        public bool HasChildren
        {
            get
            {
                return Children != null && Children.Any();
            }
        }

        public int NbChildren
        {
            get
            {
                return Children != null ? Children.Count : 0;
            }
        }

        public int NbLeaves
        {
            get
            {
                return HasChildren ? TreeLeaves.Count() : 0;
            }
        }

        public bool HasGrandChildren
        {
            get
            {
                return HasChildren && Children.Any(x => x.HasChildren);
            }
        }

        public bool CanExpand
        {
            get
            {
                return HasChildren && !IsExpanded;
            }
        }

        public bool CanCollapse
        {
            get
            {
                return HasChildren && IsExpanded;
            }
        }

        public void Expand()
        {
            IsExpanded = true;
        }

        public void ExpandAll()
        {
            Children.ToList().ForEach(x => x.ExpandAll());
            Expand();
        }

        public void Collapse()
        {
            IsExpanded = false;
        }

        public void CollapseAll()
        {
            Collapse();
            Children.ToList().ForEach(x => x.CollapseAll());
        }

        public virtual IEnumerable<TreeElementViewModel> TreeLeaves
        {
            get { return HasChildren ? Children.SelectMany(x => x.TreeLeaves) : new[] { this }; }
        }

        protected virtual void PopulateContextMenu()
        {
            ContextMenuItems.Add(new ExpandMenuItem(this));
            ContextMenuItems.Add(new CollapseMenuItem(this));
            ContextMenuItems.Add(new ExpandAllMenuItem(this));
            ContextMenuItems.Add(new CollapseAllMenuItem(this));
        }

        public void CreateTab(BaseTabViewModel tab)
        {
            OnTabCreation(this, tab);
        }
    }
}
