using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtility.Collections
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
            get { return (BinaryTreeNode<T>)m_Children[0]; }
            set
            {
                m_Children[0] = value;
                if (value != null)
                    value.m_Parent = this;
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
            get { return (BinaryTreeNode<T>)m_Children[1]; }
            set
            {
                m_Children[1] = value;
                if (value != null)
                    value.m_Parent = this;
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
            m_Children.AddRange(new BinaryTreeNode<T>[2]);
        }

        /// <summary>
        /// Retourne toute les valeurs séquentiellement (LeftNode.FamilyLeftToRight + Value + RightNode.FamilyLeftToRight)
        /// </summary>
        public IEnumerable<T> FamilyLeftToRight
        {
            get
            {
                List<T> vals = new List<T> { m_Value };
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
                List<T> vals = new List<T> { m_Value };
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
    public class BinaryTree<T> : AbstractTree<T>
    {
        /// <summary>
        /// Racine de l'arbre
        /// </summary>
        public BinaryTreeNode<T> Root
        {
            get { return (BinaryTreeNode<T>)m_Root; }
        }
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
