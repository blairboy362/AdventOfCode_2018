using System.Collections.Generic;

namespace Day09
{
    public static class ExtensionMethods
    {
        public static LinkedListNode<T> Clockwise<T>(this LinkedListNode<T> node)
        {
            return node.Next ?? node.List.First;
        }

        public static LinkedListNode<T> CounterClockwise<T>(this LinkedListNode<T> node)
        {
            return node.Previous ?? node.List.Last;
        }

        public static void AddClockwiseOf<T>(this LinkedList<T> list, LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            list.AddAfter(node, newNode);
        }

        public static void AddCounterClockwiseOf<T>(this LinkedList<T> list, LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            list.AddBefore(node, newNode);
        }
    }
}
