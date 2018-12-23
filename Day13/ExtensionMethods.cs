using System.Collections.Generic;

namespace Day13
{
    public static class ExtensionMethods
    {
        public static LinkedListNode<T> Next<T>(this LinkedListNode<T> node)
        {
            return node.Next ?? node.List.First;
        }
    }
}
