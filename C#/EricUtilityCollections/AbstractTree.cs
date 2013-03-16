using System.Collections.Generic;

namespace EricUtility.Collections
{
    /// <summary>
    /// Noeud d'arbre (Classe abstraite)
    /// </summary>
    /// <typeparam name="T">Le type d'élément</typeparam>
    public abstract class AbstractTreeNode<T>
    {
        protected T m_Value;
        protected List<AbstractTreeNode<T>> m_Children = new List<AbstractTreeNode<T>>();
        protected AbstractTreeNode<T> m_Parent;

        /// <summary>
        /// Élément du noeud
        /// </summary>
        public T Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        /// <summary>
        /// Liste des enfants
        /// </summary>
        public AbstractTreeNode<T>[] Children { get { return m_Children.ToArray(); } }

        /// <summary>
        /// Le parent direct
        /// </summary>
        public AbstractTreeNode<T> Parent { get { return m_Parent; } }

        /// <summary>
        /// Noeud d'arbre (Classe abstraite)
        /// </summary>
        /// <param name="val">Élément</param>
        /// <param name="parent">Parent direct</param>
        public AbstractTreeNode(T val, AbstractTreeNode<T> parent)
        {
            m_Value = val;
            m_Parent = parent;
        }

        /// <summary>
        /// Retourne tous les parents d'un noeud. Plus l'index est éloigné, plus l'on remonte a la racine.
        /// </summary>
        public List<AbstractTreeNode<T>> Parents
        {
            get
            {
                List<AbstractTreeNode<T>> par = new List<AbstractTreeNode<T>>();
                AbstractTreeNode<T> p = this.Parent;
                while (p != null)
                {
                    par.Add(p);
                    p = p.Parent;
                }
                return par;
            }
        }
    }

    /// <summary>
    /// Un arbre (Classe abstraite)
    /// </summary>
    /// <typeparam name="T">Le type délément</typeparam>
    public abstract class AbstractTree<T>
    {
        protected AbstractTreeNode<T> m_Root;

        /// <summary>
        /// Un arbre (Classe abstraite)
        /// </summary>
        /// <param name="root">Racine de l'arbre</param>
        public AbstractTree(AbstractTreeNode<T> root)
        {
            m_Root = root;
        }
    }
}