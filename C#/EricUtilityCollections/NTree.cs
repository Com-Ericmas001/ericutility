using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtility.Collections
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
            m_Children.Add(new NTreeNode<T>(val, this));
        }

        /// <summary>
        /// Enleve le ieme enfant
        /// </summary>
        /// <param name="i">Index de l'enfant a enlever</param>
        /// <returns>Vrai si l'on a enlevé l'enfant</returns>
        public bool RemoveChildAt(int i)
        {
            if (i < 0 || i > m_Children.Count)
                return false;
            m_Children.RemoveAt(i);
            return true;
        }

        /// <summary>
        /// Enleve l'enfant
        /// </summary>
        /// <param name="node">Enfant</param>
        /// <returns>Vrai si l'on a enlevé l'enfant</returns>
        public bool RemoveChild(NTreeNode<T> node)
        {
            return m_Children.Remove(node);
        }

        /// <summary>
        /// Enleve les enfants qui ont cet élément
        /// </summary>
        /// <param name="val">Élément</param>
        /// <returns>Vrai si l'on a enlevé au moins un enfant</returns>
        public bool RemoveAllChildren(T val)
        {
            int count = m_Children.Count;
            
            List<AbstractTreeNode<T>> oks = new List<AbstractTreeNode<T>>();
            foreach (AbstractTreeNode<T> child in m_Children)
                if (!child.Value.Equals(val))
                    oks.Add(child);
            return m_Children.Count != count;
        }
        /// <summary>
        /// Enleve tous les enfants
        /// </summary>
        /// <returns>Vrai si l'on a enlevé au moins un enfant</returns>
        public bool RemoveAllChildren()
        {
            int count = m_Children.Count;
            m_Children.Clear();
            return m_Children.Count != count;
        }
    }

    /// <summary>
    /// Arbre à plusieurs enfants
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NTree<T> : AbstractTree<T>
    {
        /// <summary>
        /// Racine de l'arbre
        /// </summary>
        public NTreeNode<T> Root
        {
            get { return (NTreeNode<T>)m_Root; }
        }
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

