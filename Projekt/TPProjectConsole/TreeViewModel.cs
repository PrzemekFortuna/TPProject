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
        public string Output
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(var line in OutputList)
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

            foreach(var item in reflection.Namespaces)
            {
                Namespaces.Add(new NamespaceViewModel(item));
                
            }

            foreach (var ns in Namespaces)
            {
                PrintChildren(ns, 1);
            }
        }

        private void PrintChildren(TreeViewItemViewModel item, int level)
        {
            string prefix = string.Empty;
            for (int i = 0; i < level; i++)
                prefix += "   ";


            OutputList.Add(item.Name);
            foreach(var child in item.Children)
            {
                if(child.Children.Count > 1)
                    OutputList.Add(prefix + child.Name);

                if (child.IsExpanded)
                    PrintChildren(child, level + 1);
            }
        }

        public void Expand()
        {
            //TODO: Implement Expand() method

            throw new NotImplementedException("Expand method");
        }
    }
}
