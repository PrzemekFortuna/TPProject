using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPProject.ViewModel;
using TPProjectLib.Reflection;

namespace TPProjectConsole
{
    public class TreeViewModel
    {
        public ObservableCollection<string> OutputList { get; set; }
        private int Level = 0;

        public string Output
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var line in OutputList)
                {
                    sb.Append(line);
                    sb.Append(Environment.NewLine);
                }

                return sb.ToString();
            }
        }
        private ReflectionModel reflection;
        public List<TreeViewItemViewModel> Namespaces { get; private set; }

        public TreeViewModel(string path)
        {
            reflection = new ReflectionModel(path);
            OutputList = new ObservableCollection<string>();
            Namespaces = new List<TreeViewItemViewModel>();
            Init();
        }

        public void Init()
        {

            foreach (var item in reflection.Namespaces)
            {
                Namespaces.Add(new NamespaceViewModel(item));
            }

            foreach (var ns in Namespaces)
            {
                
                PrintChildren(ns, 0);
            }
        }

        private void PrintChildren(TreeViewItemViewModel item, int level)
        {
            string prefix = string.Empty;
            for (int i = 0; i < level; i++)
                prefix += "   ";


            OutputList.Add(prefix + item.Name);
            foreach (var child in item.Children)
            {
                if (child.IsExpanded)
                    PrintChildren(child, level + 1);
            }
        }
        private void ExpandChildren(TreeViewItemViewModel item, int level)
        {
            int tmp = level + 1;
            if (item.Children.Count > 0 && item.Children[0].IsExpanded)
            {
                foreach (var child in item.Children)
                    ExpandChildren(child, tmp);
            }
            else if (item.Children.Count > 0)
            {
                foreach (var child in item.Children.ToList())
                    child.IsExpanded = true;
            }

        }

        private void ShrinkChildren(TreeViewItemViewModel item, int level)
        {
            int tmp = level + 1;
            if (item.Children.Count > 0 && item.Children[0].IsExpanded && tmp >= Level)
            {
                foreach(var child in item.Children)
                    child.IsExpanded = false;
            }
            else if (item.Children.Count > 0)
            {
                foreach (var child in item.Children)
                    ShrinkChildren(child, tmp);
            }
        }
        public void Expand()
        {
            Level++;
            OutputList.Clear();
            foreach (var ns in Namespaces)
            {
                ExpandChildren(ns, 0);
                PrintChildren(ns, 0);
            }

        }

        public void Shrink()
        {
            if (Level > 1)
            {
                Level--;

                OutputList.Clear();
                foreach (var ns in Namespaces)
                {

                    ShrinkChildren(ns, 0);
                    PrintChildren(ns, 0);
                }
            }
        }
    }
}
