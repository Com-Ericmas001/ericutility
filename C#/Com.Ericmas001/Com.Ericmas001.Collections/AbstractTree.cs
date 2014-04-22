using System.Collections.Generic;

namespace Com.Ericmas001.Collections
{
    /// <summary>
    /// Noeud d'arbre (Classe abstraite)
    /// </summary>
    /// <typeparam name="T">Le type d'élément</typeparam>
    public abstract class AbstractTreeNode<T>
    {
        /// <summary>
        /// Élément du noeud
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Liste des enfants
        /// </summary>
        public List<AbstractTreeNode<T>> Children { get; protected set; }

        /// <summary>
        /// Le parent direct
        /// </summary>
        public AbstractTreeNode<T> Parent { get; protected set; }

        /// <summary>
        /// Noeud d'arbre (Classe abstraite)
        /// </summary>
        /// <param name="val">Élément</param>
        /// <param name="parent">Parent direct</param>
        protected AbstractTreeNode(T val, AbstractTreeNode<T> parent)
        {
            Children = new List<AbstractTreeNode<T>>();
            Value = val;
            Parent = parent;
        }

        /// <summary>
        /// Retourne tous les parents d'un noeud. Plus l'index est éloigné, plus l'on remonte a la racine.
        /// </summary>
        public List<AbstractTreeNode<T>> Parents
        {
            get
            {
                var par = new List<AbstractTreeNode<T>>();
                var p = Parent;
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
    /// <typeparam name="TNode">Le type de Node dans l'arbre </typeparam>
    public abstract class AbstractTree<TNode>
    {
        public TNode Root { get; private set; }

        /// <summary>
        /// Un arbre (Classe abstraite)
        /// </summary>
        /// <param name="root">Racine de l'arbre</param>
        public AbstractTree(TNode root)
        {
            Root = root;
        }
    }
}