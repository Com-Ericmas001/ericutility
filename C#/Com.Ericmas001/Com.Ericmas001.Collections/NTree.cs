using System.Collections.Generic;

namespace Com.Ericmas001.Collections
{
    /// <summary>
    /// Noeud d'arbre à plusieurs enfants
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NTreeNode<T> : AbstractTreeNode<T>
    {
        /// <summary>
        /// Noeud d'arbre à plusieurs enfants
        /// </summary>
        /// <param name="val">Élément</param>
        /// <param name="parent">Parent</param>
        public NTreeNode(T val, NTreeNode<T> parent)
            : base(val, parent)
        {
        }

        /// <summary>
        /// Ajoute un enfant au noeud
        /// </summary>
        /// <param name="val">Élément</param>
        public void AddChildren(T val)
        {
            Children.Add(new NTreeNode<T>(val, this));
        }

        /// <summary>
        /// Enleve le ieme enfant
        /// </summary>
        /// <param name="i">Index de l'enfant a enlever</param>
        /// <returns>Vrai si l'on a enlevé l'enfant</returns>
        public bool RemoveChildAt(int i)
        {
            if (i < 0 || i > Children.Count)
                return false;
            Children.RemoveAt(i);
            return true;
        }

        /// <summary>
        /// Enleve l'enfant
        /// </summary>
        /// <param name="node">Enfant</param>
        /// <returns>Vrai si l'on a enlevé l'enfant</returns>
        public bool RemoveChild(NTreeNode<T> node)
        {
            return Children.Remove(node);
        }

        /// <summary>
        /// Enleve les enfants qui ont cet élément
        /// </summary>
        /// <param name="val">Élément</param>
        /// <returns>Vrai si l'on a enlevé au moins un enfant</returns>
        public bool RemoveAllChildren(T val)
        {
            var count = Children.Count;

            var oks = new List<AbstractTreeNode<T>>();
            foreach (var child in Children)
                if (!child.Value.Equals(val))
                    oks.Add(child);
            return Children.Count != count;
        }

        /// <summary>
        /// Enleve tous les enfants
        /// </summary>
        /// <returns>Vrai si l'on a enlevé au moins un enfant</returns>
        public bool RemoveAllChildren()
        {
            var count = Children.Count;
            Children.Clear();
            return Children.Count != count;
        }
    }

    /// <summary>
    /// Arbre à plusieurs enfants
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NTree<T> : AbstractTree<NTreeNode<T>>
    {
        /// <summary>
        /// Arbre à plusieurs enfants
        /// </summary>
        /// <param name="rootVal">Élément servant de racine</param>
        public NTree(T rootVal)
            : base(new NTreeNode<T>(rootVal, null))
        {
        }
    }
}