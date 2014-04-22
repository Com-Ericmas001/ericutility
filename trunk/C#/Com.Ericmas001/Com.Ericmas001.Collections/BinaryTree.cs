using System.Collections.Generic;

namespace Com.Ericmas001.Collections
{
    /// <summary>
    /// Noeud d'arbre binaire
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTreeNode<T> : AbstractTreeNode<T>
    {
        /// <summary>
        /// Enfant de gauche
        /// </summary>
        public BinaryTreeNode<T> LeftNode
        {
            get { return (BinaryTreeNode<T>)Children[0]; }
            set
            {
                Children[0] = value;
                if (value != null)
                    value.Parent = this;
            }
        }

        /// <summary>
        /// Valeur de l'enfant de gauche
        /// </summary>
        public T LeftValue
        {
            get { return LeftNode.Value; }
            set { LeftNode = new BinaryTreeNode<T>(value); }
        }

        /// <summary>
        /// Enfant de droit
        /// </summary>
        public BinaryTreeNode<T> RightNode
        {
            get { return (BinaryTreeNode<T>)Children[1]; }
            set
            {
                Children[1] = value;
                if (value != null)
                    value.Parent = this;
            }
        }

        /// <summary>
        /// Valeur de l'enfant de gauche
        /// </summary>
        public T RightValue
        {
            get { return RightNode.Value; }
            set { RightNode = new BinaryTreeNode<T>(value); }
        }

        /// <summary>
        /// Noeud d'arbre binaire
        /// </summary>
        /// <param name="val">Élément</param>
        public BinaryTreeNode(T val)
            : base(val, null)
        {
            Children.AddRange(new BinaryTreeNode<T>[2]);
        }

        /// <summary>
        /// Retourne toute les valeurs séquentiellement (LeftNode.FamilyLeftToRight + Value + RightNode.FamilyLeftToRight)
        /// </summary>
        public IEnumerable<T> FamilyLeftToRight
        {
            get
            {
                var vals = new List<T> { Value };
                if (LeftNode != null)
                    vals.AddRange(LeftNode.FamilyLeftToRight);
                if (RightNode != null)
                    vals.AddRange(RightNode.FamilyLeftToRight);
                return vals;
            }
        }

        /// <summary>
        /// Retourne toute les valeurs séquentiellement (RightNode.FamilyRightToLeft + Value + LeftNode.FamilyRightToLeft)
        /// </summary>
        public IEnumerable<T> FamilyRightToLeft
        {
            get
            {
                var vals = new List<T> { Value };
                if (RightNode != null)
                    vals.AddRange(RightNode.FamilyRightToLeft);
                if (LeftNode != null)
                    vals.AddRange(LeftNode.FamilyRightToLeft);
                return vals;
            }
        }
    }

    /// <summary>
    /// Arbre binaire (possédant toujours que 2 enfants)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTree<T> : AbstractTree<BinaryTreeNode<T>>
    {
        /// <summary>
        /// Arbre binaire (possédant toujours que 2 enfants)
        /// </summary>
        /// <param name="rootVal">Élément servant de racine</param>
        public BinaryTree(T rootVal)
            : base(new BinaryTreeNode<T>(rootVal))
        {
        }

        /// <summary>
        /// Retourne tous les éléments sur un parcours de gauche a droite
        /// </summary>
        public IEnumerable<T> AllValuesLeftToRight { get { return Root.FamilyLeftToRight; } }

        /// <summary>
        /// Retourne tous les éléments sur un parcours de droite a gauche
        /// </summary>
        public IEnumerable<T> AllValuesRightToLeft { get { return Root.FamilyRightToLeft; } }
    }
}